void LightsDirection_float(float3 worldPos, int index, out float3 Direction) {
    
    #ifndef SHADERGRAPH_PREVIEW
    
        Direction = normalize(float3(0.5f, 0.5f, 0.25f));
        index = 0;
    
    #else 
        
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        
        int lightCount = GetAdditionalLightsCount();
    
        if (index < lightCount) {
            Light light = GetAdditionalLight(index, worldPos);
        
            Direction = light.direction;
        }        
    
    #endif 
}
