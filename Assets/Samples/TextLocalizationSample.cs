using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Localization.Samples
{
    public class TextLocalizationSample : MonoBehaviour
    {
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
            LocalizationManager.Instance.CurrentLocale = Locales.Spain;
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
            var enLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/en.bytes");
            var cnLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/zh_CN.bytes");
            var twLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/zh_TW.bytes");
            var esLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/es_ES.bytes");
            LocalizationManager.Instance.LocaleChanged += OnLocaleChanged;
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.English, enLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.SimplifiedChinese, cnLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.TraditionalChinese, twLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.Spain, esLang.bytes);
            LocalizationManager.Instance.CurrentLocale = Locales.English;
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            UpdateText(text1, LocalizationManager.Instance.GetTranslationData(TranslationKey.Hello));
            UpdateText(text2, LocalizationManager.Instance.GetTranslationData(TranslationKey.Thanks));
            UpdateText(text3, LocalizationManager.Instance.GetTranslationData(TranslationKey.GoodBye));
            UpdateText(text4, LocalizationManager.Instance.GetTranslationData(TranslationKey.LoveYou));
        }

        private void UpdateText(Text textField, TranslationData translationData)
        {
            if (translationData.Style is { Length: > 2 })
            {
                if (int.TryParse(translationData.Style[2], out var fontSize))
                    textField.fontSize = fontSize;
            }
            
            textField.text = translationData.Text;
        }
    }
}