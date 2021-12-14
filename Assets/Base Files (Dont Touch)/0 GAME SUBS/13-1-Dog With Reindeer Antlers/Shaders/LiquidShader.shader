// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33822,y:32654,varname:node_3138,prsc:2|emission-8593-OUT,alpha-7562-OUT;n:type:ShaderForge.SFN_Tex2d,id:9998,x:32600,y:32586,ptovrint:False,ptlb:Main Tex,ptin:_MainTex,varname:node_9998,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_DDXY,id:6601,x:32782,y:32892,varname:node_6601,prsc:2|IN-7792-A;n:type:ShaderForge.SFN_Multiply,id:5897,x:32971,y:32892,varname:node_5897,prsc:2|A-6601-OUT,B-5839-OUT;n:type:ShaderForge.SFN_Vector1,id:5839,x:32782,y:33031,varname:node_5839,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:1859,x:33152,y:33031,varname:node_1859,prsc:2|A-9040-OUT,B-5897-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9040,x:32971,y:33031,ptovrint:False,ptlb:Threshold,ptin:_Threshold,varname:node_9040,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:2772,x:33152,y:32892,varname:node_2772,prsc:2|A-9040-OUT,B-5897-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:9096,x:33360,y:32851,varname:node_9096,prsc:2|IN-7792-A,IMIN-2772-OUT,IMAX-1859-OUT,OMIN-2016-OUT,OMAX-1323-OUT;n:type:ShaderForge.SFN_Vector1,id:2016,x:33152,y:33157,varname:node_2016,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1323,x:33152,y:33223,varname:node_1323,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp,id:7562,x:33557,y:32851,varname:node_7562,prsc:2|IN-9096-OUT,MIN-2016-OUT,MAX-1323-OUT;n:type:ShaderForge.SFN_Tex2d,id:7792,x:32588,y:32767,ptovrint:False,ptlb:Blurs,ptin:_Blurs,varname:node_7792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Divide,id:3532,x:32952,y:32757,varname:node_3532,prsc:2|A-7792-RGB,B-7792-A;n:type:ShaderForge.SFN_Clamp,id:8593,x:33360,y:32721,varname:node_8593,prsc:2|IN-3532-OUT,MIN-2016-OUT,MAX-1323-OUT;proporder:9998-9040-7792;pass:END;sub:END;*/

Shader "Shader Forge/LiquidShader" {
    Properties {
        _MainTex ("Main Tex", 2D) = "white" {}
        _Threshold ("Threshold", Float ) = 0.5
        _Blurs ("Blurs", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0
            uniform sampler2D _Blurs; uniform float4 _Blurs_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Threshold)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 _Blurs_var = tex2D(_Blurs,TRANSFORM_TEX(i.uv0, _Blurs));
                float node_2016 = 0.0;
                float node_1323 = 1.0;
                float3 emissive = clamp((_Blurs_var.rgb/_Blurs_var.a),node_2016,node_1323);
                float3 finalColor = emissive;
                float _Threshold_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Threshold );
                float node_5897 = (fwidth(_Blurs_var.a)*0.5);
                float node_2772 = (_Threshold_var-node_5897);
                return fixed4(finalColor,clamp((node_2016 + ( (_Blurs_var.a - node_2772) * (node_1323 - node_2016) ) / ((_Threshold_var+node_5897) - node_2772)),node_2016,node_1323));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
