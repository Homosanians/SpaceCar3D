﻿//referenced:https://github.com/you-ri/LiliumToonGraph/blob/master/Packages/jp.lilium.toongraph/Editor/ShaderGraph/ToonOutlinePass.hlsl

#ifndef TOON_OUTLINEPASS_INCLUDED
#define TOON_OUTLINEPASS_INCLUDED

struct Attributes
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;
#ifdef _USESMOOTHNORMAL
    float4 tangentOS : TANGENT;
    float2 texcoord7 : TEXCOORD7;
#endif
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float4 positionCS : SV_POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

float4 TransformOutlineToHClipScreenSpace(float4 position, float3 normal, float outlineWidth)
{
    half _OutlineScaledMaxDistance = 10;

    float4 nearUpperRight = mul(unity_CameraInvProjection, float4(1, 1, UNITY_NEAR_CLIP_VALUE, _ProjectionParams.y));
    float aspect = abs(nearUpperRight.y / nearUpperRight.x);
    float4 vertex = TransformObjectToHClip(position);
#ifdef _USESMOOTHNORMAL
    float3 viewNormal = TransformWorldToViewDir(normal);
#else
    float3 viewNormal = mul((float3x3) UNITY_MATRIX_IT_MV, normal.xyz);
#endif
    float3 clipNormal = mul((float3x3) UNITY_MATRIX_P,viewNormal.xyz);
    float2 projectedNormal = normalize(clipNormal.xy);
    projectedNormal *= min(vertex.w, _OutlineScaledMaxDistance);
    projectedNormal.x *= aspect;
    vertex.xy += 0.01 * outlineWidth * projectedNormal.xy;
    return vertex;
}

Varyings Vertex(Attributes input)
{
    Varyings output = (Varyings) 0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
#ifdef _USESMOOTHNORMAL
    float3 normalDir = TransformObjectToWorldNormal(input.normalOS);
    float3 tangentDir = TransformObjectToWorldDir(input.tangentOS.xyz);
    float3 bitangentDir = normalize(cross(normalDir, tangentDir) * input.tangentOS.w);
    float3x3 t_tbn = float3x3(tangentDir,bitangentDir,normalDir);
    float3 bakeNormal = GetSmoothedWorldNormal(input.texcoord7,t_tbn);
    output.positionCS = TransformOutlineToHClipScreenSpace(input.positionOS, bakeNormal, _OutlineWidth);
#else
    output.positionCS = TransformOutlineToHClipScreenSpace(input.positionOS, input.normalOS, _OutlineWidth);
#endif
    return output;
}

half4 Fragment(Varyings input) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
    return _OutlineColor;
}
#endif