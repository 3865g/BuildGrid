Shader "PDT Shaders/TestGridUNLIT"
{
    Properties
    {
        _LineColor("Line Color", Color) = (1,1,1,1)
        _CellColor("Cell Color", Color) = (0,0,0,0)
        _SelectedColor("Selected Color", Color) = (1,0,0,1)
        [PerRendererData] _MainTex("Albedo (RGB)", 2D) = "white" {}
        [IntRange] _GridSize("Grid Size", Range(1,100)) = 10
        _LineSize("Line Size", Range(0,1)) = 0.15
        [IntRange] _SelectCell("Select Cell Toggle ( 0 = False , 1 = True )", Range(0,1)) = 0.0
        [IntRange] _SelectedCellX("Selected Cell X", Range(0,100)) = 0.0
        [IntRange] _SelectedCellY("Selected Cell Y", Range(0,100)) = 0.0
        _GridWidth("Grid Width", Float) = 10.0 // Ширина сетки
        _GridHeight("Grid Height", Float) = 10.0 // Высота сетки
    }

    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1; // Мировые координаты
                float4 vertex : SV_POSITION;
            };

            half _Glossiness = 0.0;
            half _Metallic = 0.0;
            float4 _LineColor;
            float4 _CellColor;
            float4 _SelectedColor;
            sampler2D _MainTex;

            float _GridSize;
            float _LineSize;

            float _SelectCell;
            float _SelectedCellX;
            float _SelectedCellY;

            float _GridWidth; // Ширина сетки
            float _GridHeight; // Высота сетки

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); // Преобразуем в мировые координаты
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 worldUV = i.worldPos.xy; // Используем мировые координаты

                // Вычисляем смещение для центрирования сетки
                float gridOffsetX = -_GridWidth * _GridSize / 2.0;
                float gridOffsetY = -_GridHeight * _GridSize / 2.0;

                // Применяем смещение сетки
                worldUV.x += gridOffsetX;
                worldUV.y += gridOffsetY;

                _SelectedCellX = floor(_SelectedCellX);
                _SelectedCellY = floor(_SelectedCellY);

                fixed4 c = float4(0.0, 0.0, 0.0, 0.0);

                float brightness = 1.0;

                // Размер клетки без учета LineSize
                float gsize = _GridSize;

                // Идентификаторы клеток
                float2 id;
                id.x = floor(worldUV.x / gsize);
                id.y = floor(worldUV.y / gsize);

                // Позиция внутри клетки
                float2 cellUV = float2(
                    frac(worldUV.x / gsize),
                    frac(worldUV.y / gsize)
                );

                float4 color = _CellColor;
                brightness = _CellColor.w;

                // Проверяем, выбрана ли ячейка
                if (round(_SelectCell) == 1.0 && id.x == _SelectedCellX && id.y == _SelectedCellY)
                {
                    brightness = _SelectedColor.w;
                    color = _SelectedColor;
                }

                // Рисуем линии сетки
                if (cellUV.x <= _LineSize || cellUV.x >= (1.0 - _LineSize) ||
                    cellUV.y <= _LineSize || cellUV.y >= (1.0 - _LineSize))
                {
                    brightness = _LineColor.w;
                    color = _LineColor;
                }

                // Отсекаем прозрачные пиксели
                if (brightness == 0.0) {
                    clip(c.a - 1.0);
                }

                c = fixed4(color.x * brightness, color.y * brightness, color.z * brightness, brightness);
                return c;
            }
            ENDCG
        }
    }
}