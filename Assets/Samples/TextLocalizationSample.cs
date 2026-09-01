using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Localization.Samples
{
    public class TextLocalizationSample : MonoBehaviour
    {
        [SerializeField]
        private TextAsset[] localeTextAssets;
        
        [SerializeField]
        private Text text1;

        [SerializeField]
        private Text text2;

        [SerializeField]
        private Text text3;

        [SerializeField]
        private Text text4;

        public void OnButton1Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.English;
        }

        public void OnButton2Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.SimplifiedChinese;
        }

        public void OnButton3Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.TraditionalChinese;
        }

        public void OnButton4Clicked()
        {
            LocalizationManager.Instance.CurrentLocale = Locales.Spanish;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.LocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            UpdateTexts();
        }

        // Start is called before the first frame update
        private void Start()
        {
            LocalizationManager.Instance.LocaleChanged += OnLocaleChanged;
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.English, localeTextAssets[0].bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.SimplifiedChinese, localeTextAssets[1].bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.TraditionalChinese, localeTextAssets[2].bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.Spanish, localeTextAssets[3].bytes);
            LocalizationManager.Instance.CurrentLocale = Locales.English;
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            UpdateText(text1, LocalizationManager.Instance.GetTranslationData(TranslationKey.Hello));
            UpdateText(text2, LocalizationManager.Instance.GetTranslationData(TranslationKey.Thanks));
            UpdateText(text3, LocalizationManager.Instance.GetTranslationData(TranslationKey.GoodBye));
            UpdateText(text4, LocalizationManager.Instance.GetTranslationData(TranslationKey.LoveYou), "Julia");
        }

        private static void UpdateText(Text textField, TranslationData translationData, string name = null)
        {
            if (translationData == null)
                return;

            translationData.TryGetStyleParameter("fontSize", out var fontSizeString);
            if (!string.IsNullOrEmpty(fontSizeString) && int.TryParse(fontSizeString, out var fontSize))
                textField.fontSize = fontSize;

            textField.text = !string.IsNullOrEmpty(name) ? translationData.GetFormattedText(name) : translationData.Text;
        }
    }
}