using UniSharper.Localization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Localization.Example
{
    public class LocalizationExample : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Text text1 = null;

        [SerializeField]
        private Text text2 = null;

        [SerializeField]
        private Text text3 = null;

        #endregion Fields

        #region Methods

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
            TextAsset enLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/en.bytes");
            TextAsset cnLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/zh_CN.bytes");
            TextAsset twLang = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Localization/Locales/zh_TW.bytes");
            LocalizationManager.Instance.LocaleChanged += OnLocaleChanged;
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.English, enLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.SimplifiedChinese, cnLang.bytes);
            LocalizationManager.Instance.LoadLocalizationAssetData(Locales.TraditionalChinese, twLang.bytes);
            LocalizationManager.Instance.CurrentLocale = Locales.English;
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            text1.text = LocalizationManager.Instance.GetTranslationText(TranslationKey.Hello);
            text2.text = LocalizationManager.Instance.GetTranslationText(TranslationKey.Thanks);
            text3.text = LocalizationManager.Instance.GetTranslationText(TranslationKey.GoodBye);
        }

        #endregion Methods
    }
}