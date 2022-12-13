using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Localization.Samples
{
    public class TMPTextLocalizationSample : MonoBehaviour
    {
        [SerializeField]
        private TextAsset[] localeTextAssets;
        
        [SerializeField]
        private TextMeshProUGUI text1;
        
        [SerializeField]
        private TextMeshProUGUI text2;
        
        [SerializeField]
        private TextMeshProUGUI text3;
        
        [SerializeField]
        private TextMeshProUGUI text4;

        [SerializeField]
        private TMP_FontAsset[] fontAssets;
        
        [SerializeField]
        private Material[] fontMaterials;

        [SerializeField]
        private TMP_ColorGradient[] colorGradientPresets;

        [SerializeField]
        private Button button1;
        
        [SerializeField]
        private Button button2;
        
        [SerializeField]
        private Button button3;
        
        [SerializeField]
        private Button button4;

        private void Start()
        {
            LocalizationManager.Instance.LocaleChanged += OnLocaleChanged;
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.English, localeTextAssets[0].bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.SimplifiedChinese, localeTextAssets[1].bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.TraditionalChinese, localeTextAssets[2].bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.Spanish, localeTextAssets[3].bytes);
            LocalizationManager.Instance.CurrentLocale = Locales.English;
            UpdateTexts();
            
            if (button1)
                button1.onClick.AddListener(OnButton1Clicked);
            
            if (button2)
                button2.onClick.AddListener(OnButton2Clicked);
            
            if (button3)
                button3.onClick.AddListener(OnButton3Clicked);
            
            if (button4)
                button4.onClick.AddListener(OnButton4Clicked);
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.LocaleChanged -= OnLocaleChanged;
            
            if (button1)
                button1.onClick.RemoveListener(OnButton1Clicked);
            
            if (button2)
                button2.onClick.RemoveListener(OnButton2Clicked);
            
            if (button3)
                button3.onClick.RemoveListener(OnButton3Clicked);
            
            if (button4)
                button4.onClick.RemoveListener(OnButton4Clicked);
        }
        
        private void OnLocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            UpdateTexts();
        }

        private void OnButton1Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.English;
        }

        private void OnButton2Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.SimplifiedChinese;
        }

        private void OnButton3Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.TraditionalChinese;
        }

        private void OnButton4Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.Spanish;
        }
        
        private void UpdateTexts()
        {
            var text1TranslationData = LocalizationManager.Instance.GetTranslationData(TranslationKey.Hello);
            var text2TranslationData = LocalizationManager.Instance.GetTranslationData(TranslationKey.Thanks);
            var text3TranslationData = LocalizationManager.Instance.GetTranslationData(TranslationKey.GoodBye);
            var text4TranslationData = LocalizationManager.Instance.GetTranslationData(TranslationKey.LoveYou);

            UpdateText(text1, text1TranslationData);
            UpdateText(text2, text2TranslationData);
            UpdateText(text3, text3TranslationData);
            UpdateText(text4, text4TranslationData);
        }

        private void UpdateText(TextMeshProUGUI textField, TranslationData translationData)
        {
            if (translationData == null)
                return;

            if (translationData.TryGetStyleParameter("font", out var font) && !string.IsNullOrEmpty(font))
            {
                // font
                var fontAsset = GetFontAsset(font);
                if (fontAsset)
                    textField.font = fontAsset;
                
                // fontSize
                if (translationData.TryGetStyleParameter("fontSize", out var fontSizeString))
                {
                    if (int.TryParse(fontSizeString, out var fontSize)) 
                        textField.fontSize = fontSize;
                }

                // fontMaterial
                if (translationData.TryGetStyleParameter("fontMaterial", out var fontMaterialString))
                {
                    var fontMaterial = GetFontMaterial(fontMaterialString);
                    if (fontMaterial) 
                        textField.fontSharedMaterial = fontMaterial;
                }
                
                // colorGradientPreset
                if (translationData.TryGetStyleParameter("colorGradientPreset", out var colorGradientPresetString))
                {
                    var colorGradientPreset = GetColorGradientPreset(colorGradientPresetString);
                    if (colorGradientPreset)
                    {
                        textField.enableVertexGradient = true;
                        textField.colorGradientPreset = colorGradientPreset;
                    }
                }
            }

            textField.text = translationData.Text;
        }

        private TMP_FontAsset GetFontAsset(string font)
        {
            if (string.IsNullOrEmpty(font) || fontAssets == null || fontAssets.Length == 0)
                return null;
            
            return fontAssets.FirstOrDefault(fontAsset => fontAsset.name == font);
        }

        private Material GetFontMaterial(string name)
        {
            if (string.IsNullOrEmpty(name) || fontMaterials == null || fontMaterials.Length == 0)
                return null;

            return fontMaterials.FirstOrDefault(fontMaterial => fontMaterial.name == name);
        }

        private TMP_ColorGradient GetColorGradientPreset(string name)
        {
            if (string.IsNullOrEmpty(name) || colorGradientPresets == null || colorGradientPresets.Length == 0)
                return null;
            
            return colorGradientPresets.FirstOrDefault(colorGradient => colorGradient.name == name);
        }
    }
}