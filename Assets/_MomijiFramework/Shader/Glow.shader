// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33303,y:32685,varname:node_4013,prsc:2|diff-7682-OUT,normal-4693-RGB,emission-8944-OUT;n:type:ShaderForge.SFN_Tex2d,id:4538,x:33004,y:32316,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_4538,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7682,x:33004,y:32542,varname:node_7682,prsc:2|A-4538-RGB,B-4178-OUT;n:type:ShaderForge.SFN_Vector1,id:4178,x:33004,y:32481,varname:node_4178,prsc:2,v1:1.3;n:type:ShaderForge.SFN_OneMinus,id:3228,x:32393,y:32657,varname:node_3228,prsc:2|IN-4693-B;n:type:ShaderForge.SFN_Power,id:5346,x:32564,y:32743,varname:node_5346,prsc:2|VAL-3228-OUT,EXP-9594-OUT;n:type:ShaderForge.SFN_Multiply,id:8944,x:33005,y:32872,varname:node_8944,prsc:2|A-2349-RGB,B-5313-OUT,C-6860-OUT;n:type:ShaderForge.SFN_Multiply,id:5313,x:32777,y:32872,varname:node_5313,prsc:2|A-5346-OUT,B-6180-R;n:type:ShaderForge.SFN_Vector1,id:9594,x:32393,y:32819,varname:node_9594,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Color,id:2349,x:32777,y:32719,ptovrint:False,ptlb:Glow Color,ptin:_GlowColor,varname:node_2349,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:2569,x:32214,y:32931,varname:node_2569,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:4903,x:32393,y:32931,varname:node_4903,prsc:2,spu:0.1,spv:0|UVIN-2569-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6180,x:32564,y:32931,ptovrint:False,ptlb:Glow Pattern,ptin:_GlowPattern,varname:node_6180,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-4903-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4693,x:32395,y:32487,ptovrint:False,ptlb:Bump,ptin:_Bump,varname:node_4693,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Time,id:8922,x:32389,y:33369,varname:node_8922,prsc:2;n:type:ShaderForge.SFN_Frac,id:2955,x:32565,y:33369,varname:node_2955,prsc:2|IN-3750-OUT;n:type:ShaderForge.SFN_Subtract,id:8190,x:32777,y:33369,varname:node_8190,prsc:2|A-2955-OUT,B-3958-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3958,x:32777,y:33519,ptovrint:False,ptlb:node_3958,ptin:_node_3958,varname:node_3958,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Abs,id:2350,x:33006,y:33369,varname:node_2350,prsc:2|IN-8190-OUT;n:type:ShaderForge.SFN_Multiply,id:3393,x:33006,y:33227,varname:node_3393,prsc:2|A-2350-OUT,B-6751-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6751,x:32777,y:33250,ptovrint:False,ptlb:node_6751,ptin:_node_6751,varname:node_6751,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:6860,x:33006,y:33021,varname:node_6860,prsc:2|A-3393-OUT,B-3837-OUT;n:type:ShaderForge.SFN_Vector1,id:3837,x:33006,y:33165,varname:node_3837,prsc:2,v1:10;n:type:ShaderForge.SFN_Divide,id:3750,x:32565,y:33503,varname:node_3750,prsc:2|A-8922-T,B-7395-OUT;n:type:ShaderForge.SFN_Vector1,id:7395,x:32389,y:33570,varname:node_7395,prsc:2,v1:5;proporder:4538-6180-2349-4693-3958-6751;pass:END;sub:END;*/

Shader "Shader Forge/Glow" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "bump" {}
        _GlowPattern ("Glow Pattern", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (0.7,0,0,1)
        _Bump ("Bump", 2D) = "bump" {}
        _node_3958 ("node_3958", Float ) = 0.5
        _node_6751 ("node_6751", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _GlowColor;
            uniform sampler2D _GlowPattern; uniform float4 _GlowPattern_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform float _node_3958;
            uniform float _node_6751;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(i.uv0, _Bump)));
                float3 normalLocal = _Bump_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = (_Diffuse_var.rgb*1.3);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_9152 = _Time;
                float2 node_4903 = (i.uv0+node_9152.g*float2(0.1,0));
                float4 _GlowPattern_var = tex2D(_GlowPattern,TRANSFORM_TEX(node_4903, _GlowPattern));
                float4 node_8922 = _Time;
                float3 emissive = (_GlowColor.rgb*(pow((1.0 - _Bump_var.b),0.8)*_GlowPattern_var.r)*((abs((frac((node_8922.g/5.0))-_node_3958))*_node_6751)*10.0));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _GlowColor;
            uniform sampler2D _GlowPattern; uniform float4 _GlowPattern_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform float _node_3958;
            uniform float _node_6751;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(i.uv0, _Bump)));
                float3 normalLocal = _Bump_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = (_Diffuse_var.rgb*1.3);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
