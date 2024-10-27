#ifndef TEXTURECONVOLUTION_HLSL
#define TEXTURECONVOLUTION_HLSL
void TextureConvolution_float(float4 sampleGradient, float2 uv, float2 screenRes, int3x3 convMatrix, out float4 Out)
{
    // Calcola l'offset per i pixel adiacenti
    float2 offset = 1.0 / screenRes;

    // Inizializza il risultato della convoluzione
    float4 result = float4(0, 0, 0, 0);

    // Esegui la convoluzione
    for (int y = -1; y <= 1; y++)
    {
        for (int x = -1; x <= 1; x++)
        {
            float2 sampleUV = uv + float2(x, y) * offset;
            //sampleGradient = SAMPLE_TEXTURE2D(MainTex, ss, sampleUV);
            result += sampleGradient * convMatrix[y + 1][x + 1];
        }
    }

    // Assegna il risultato alla variabile di output
    Out = result;
}
#endif 