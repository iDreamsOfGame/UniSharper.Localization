using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Localization.Samples
{
    public class LocalizationSample : MonoBehaviour
    {
        [SerializeField]
        private Text text1;

        [SerializeField]
        private Text text2;

        [SerializeField]
        private Text text3;

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
            LocalizationManager.Instance.CurrentLocale = Locales.BE;
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
            var beLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/nl_be.bytes");
            LocalizationManager.Instance.LocaleChanged += OnLocaleChanged;
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.English, enLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.SimplifiedChinese, cnLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.TraditionalChinese, twLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.BE, beLang.bytes);
            LocalizationManager.Instance.CurrentLocale = Locales.English;
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            text1.text = LocalizationManager.Instance.GetTranslationText(TranslationKey.Hello);
            text2.text = LocalizationManager.Instance.GetTranslationText(TranslationKey.Thanks);
            text3.text = LocalizationManager.Instance.GetTranslationText(TranslationKey.GoodBye);
        }
    }
}