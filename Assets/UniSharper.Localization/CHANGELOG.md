# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [4.2.2] - 2026-09-03

### Changed

- Uses **string.Intern** method to reduce memory usage.



## [4.2.0] - 2026-09-02

### ⚠️ BREAKING CHANGES

- Since **[MasterMemory](https://github.com/Cysharp/MasterMemory)** is not compatible with Unity IL2CPP, so replace it with **[MemoryPack](https://github.com/Cysharp/MemoryPack)**.



### Added

- Restores **LocalizationAssetsViewerWindow**, and rename to **TranslationDataViewerWindow**.



## [4.1.3] - 2026-09-01

### Fixed

- Fixed runtime error when use with **IL2CPP**.



## [4.1.0] - 2026-09-01

### Added

- Adds dependency **[ZString](https://github.com/Cysharp/ZString)** to handle formatted string with high performance.
- Adds methods **TranslationData.GetFormattedText** to get formatted text by **ZString**.
- Adds methods **LocalizationManager.GetFormattedTranslationText** to get formatted translation text.



## [4.0.0] - 2026-08-31

### ⚠️ BREAKING CHANGES

- Use **[MasterMemory](https://github.com/Cysharp/MasterMemory)** to store translation key/value and styles data.



### Removed

- Removes **LocalizationAssetsViewerWindow**.



## [3.5.0] - 2026-06-09

### Changed

- Removes the prefix "Unity" of assembly definition files.



## [3.4.2] - 2026-04-02

### Changed

- Updates the version of dependency **io.github.idreamsofgame.unisharper.core**.



## [3.4.1] - 2026-03-25

### Added

- **Export Characters Options** adds new character sets **General Standard Chinese Characters Level 1~3**.



## [3.3.0] - 2025-12-02

### Added

- **Export Characters Text File** supports adding custom characters.



## [3.2.0] - 2025-11-26

### Added

- **Export Characters Text File** supports adding extra characters.



### Fixed

- Fixed build errors on platforms **iOS/Android/WebGL**.



## [3.1.0] - 2025-10-17

### Added

- **Font Subset Creator** supports custom font subset file name.
- **Font Subset Creator** supports different character set type.



## [3.0.0] - 2025-10-17

### Added

- Adds new font tool to create font subset file.



## [2.5.0] - 2025-10-10

### Added

- Supports exporting characters text file.



## [2.4.12] - 2025-08-13

### Changed

- The settings file **LocalizationAssetSettings** can be placed anywhere in project.



## [2.4.9] - 2024-07-02

### Fixed

- Fixed compilation errors in Unity 2019.4.x.



## [2.4.0] - 2023-11-24

### Added

- **Localization Assets Path** supports package path, e.g. **Packages/com.xxx.xxx**.

- **Localization Scripts Store Path** supports package path, e.g. **Packages/com.xxx.xxx**.



## [2.2.0] - 2021-07-22

### Added

- Adds setting field **targetLocales** for the list of Locales that only need to be built.

- Adds setting filed **excludedLocales** for the list of Locales that should be excluded in the build.



## [2.0.0] - 2021-06-30

### Added

- Support changing TextMeshProUGUI parameters at runtime.



## [1.2.0] - 2020-04-24

### Added

- Adds parameters in **LocalizationAssetSettings** to set up where to locate locale data, translation key data.

- Adds parameters in **LocalizationAssetSettings** to set tup from which row and column index to locate translation texts.



## [1.0.1] - 2020-04-24

### Fixed

- Fixed the error of scripts generation when using as unity package.



## [1.0.0] - 2020-04-23

- Initial unity project.