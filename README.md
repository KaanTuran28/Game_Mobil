# Game_Mobil

![Unity](https://img.shields.io/badge/Unity-6000.3_LTS-black?logo=unity)
![URP](https://img.shields.io/badge/Render_Pipeline-URP-2196F3)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Platform](https://img.shields.io/badge/platform-Android-3DDC84?logo=android&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)

<p align="center"><b><a href="#english">English</a></b> · <b><a href="#türkçe">Türkçe</a></b></p>

---

## English

A Unity 6 (6000.3 LTS) URP-based Android mobile game project. The repository ships
with a clean, layered project layout, a manager/event-bus architecture, and
documented coding and Git conventions so the project scales cleanly as features
are added.

### Project structure

```
Assets/
├── Animations/          # Animator Controllers, Animation Clips
├── Audio/
│   ├── Music/           # Background music
│   └── SFX/             # Sound effects
├── Materials/           # Material assets
├── Models/              # 3D model / mesh files (fbx, obj)
├── Prefabs/
│   ├── Characters/      # Player character prefabs
│   ├── Environment/     # Scene / environment prefabs
│   ├── UI/              # UI prefabs
│   └── Enemies/         # Enemy prefabs
├── Resources/           # Assets loaded at runtime via Resources.Load
├── Scenes/              # Unity scenes (Main.unity is the entry scene)
├── Scripts/
│   ├── Core/            # Core infrastructure (bootstrap, service location, event bus, state machine)
│   ├── Managers/        # Singleton managers: GameManager, SceneManager, AudioManager
│   ├── Player/          # Player behaviour scripts
│   ├── Enemy/           # Enemy AI / behaviour scripts
│   ├── UI/              # UI controllers
│   ├── Systems/         # Independent game systems (inventory, quest, save/load)
│   ├── Utilities/       # Helper classes, extension methods
│   └── Data/            # ScriptableObject definitions, data models
├── Settings/            # URP Renderer / Pipeline Asset, Volume Profile settings
├── Sprites/             # 2D sprites
├── Textures/            # Texture files
├── UI/                  # UI Toolkit / UI Document resources
├── Addressables/        # Addressable Asset System content
└── StreamingAssets/     # Files copied as-is at build time
```

### Folder reference

| Folder | Purpose |
|---|---|
| `Animations` | All Animator Controller and Animation Clip assets |
| `Audio` | Music and sound effects, split into sub-folders |
| `Materials` | Materials used in scenes and on objects |
| `Models` | Externally imported 3D models |
| `Prefabs` | Prefabs grouped by category |
| `Resources` | Assets that require dynamic runtime loading (use sparingly) |
| `Scenes` | Game scenes |
| `Scripts` | C# code split by layer / responsibility |
| `Settings` | URP and graphics settings |
| `Sprites` / `Textures` | 2D visual assets |
| `UI` | UI Toolkit documents and style files |
| `Addressables` | Groups managed by the Addressables system |
| `StreamingAssets` | Raw files that must be copied unchanged per platform |

### Setup

1. Install **Unity 6000.3.19f1** (or a newer release on the same LTS line) via Unity Hub, with the Android Build Support module (including SDK & NDK Tools and OpenJDK).
2. Add the project to Unity Hub with `Add` and open it.
3. On first open the Package Manager downloads the packages (Cinemachine included). An internet connection is required.
4. Run `Window > TextMeshPro > Import TMP Essential Resources` once (TMP ships inside `com.unity.ugui`, but Essentials must be imported separately).
5. Switch the build target with `File > Build Settings > Android` → `Switch Platform`.

### Git conventions

- `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `obj/` and build output are never committed (defined in `.gitignore`).
- Asset Serialization must stay on **Force Text** and Version Control mode on **Visible Meta Files**, so `.meta` files remain diffable and merge conflicts are easier to resolve.
- Scene and prefab files should not be edited by more than one person at a time — keep scenes modular (additive scenes) to reduce conflicts where possible.
- Every `.meta` file must be committed together with its asset; never leave one behind.
- When a new Unity package is added, commit `Packages/manifest.json` and `Packages/packages-lock.json` together.

### Branch strategy

- `main` — always releasable, stable state.
- `develop` — main integration branch where active development merges.
- `feature/<short-description>` — new feature work (e.g. `feature/player-movement`).
- `bugfix/<short-description>` — bug fixes on `develop`.
- `hotfix/<short-description>` — urgent production fixes on `main`.
- `release/<version>` — pre-release stabilisation (e.g. `release/0.2.0`).

Flow: `feature/*` → PR → `develop` → `release/*` → `main`.

### Commit naming

[Conventional Commits](https://www.conventionalcommits.org/) are used:

```
<type>(<scope>): <short description>
```

Examples:

- `feat(player): add double-jump mechanic`
- `fix(ui): fix button click delay on the main menu`
- `refactor(systems): restructure inventory system around SOLID`
- `perf(rendering): reduce draw calls on mobile`
- `chore(project): organise folder structure and project settings`
- `docs(readme): update setup steps`

Types used: `feat`, `fix`, `refactor`, `perf`, `chore`, `docs`, `test`, `style`.

### Coding standards

See [`CODING_STANDARDS.md`](CODING_STANDARDS.md) for details. Summary:

- Classes, methods and public members: `PascalCase`
- Private fields: `_camelCase`
- Prefer `[SerializeField] private` + property over public fields
- XML doc comments (`/// <summary>`) on public APIs
- Separate logical sections in large classes with `#region`
- Architecture follows SOLID; keep a clear separation of responsibility between the `Managers`, `Systems` and `Data` layers

### Architecture

See [`ARCHITECTURE.md`](ARCHITECTURE.md) for the manager structures (Game/Audio/Pool/Save/UI Manager), the Event Bus and their dependency graph. Summary: every manager is set up by a `Bootstrapper` (composition root) and accessed through its interface via a `ServiceLocator`; gameplay/UI code communicates over the Event Bus instead of calling managers directly.

### License

MIT — see [LICENSE](./LICENSE).

---

## Türkçe

Unity 6 (6000.3 LTS) URP tabanlı Android mobil oyun projesi. Depo; temiz ve katmanlı
bir proje düzeni, manager/event-bus mimarisi ve belgelenmiş kod/Git kurallarıyla
gelir; böylece yeni özellikler eklendikçe proje düzenli bir şekilde büyür.

### Proje Yapısı

```
Assets/
├── Animations/          # Animator Controller'lar, Animation Clip'ler
├── Audio/
│   ├── Music/           # Arka plan müzikleri
│   └── SFX/             # Ses efektleri
├── Materials/            # Material dosyaları
├── Models/               # 3D model / mesh dosyaları (fbx, obj)
├── Prefabs/
│   ├── Characters/       # Oyuncu karakter prefab'ları
│   ├── Environment/      # Sahne / çevre prefab'ları
│   ├── UI/               # UI prefab'ları
│   └── Enemies/          # Düşman prefab'ları
├── Resources/            # Runtime'da Resources.Load ile erişilen varlıklar
├── Scenes/               # Unity sahneleri (Main.unity ana sahne)
├── Scripts/
│   ├── Core/             # Uygulama genelinde temel altyapı (bootstrap, servis lokasyonu vb.)
│   ├── Managers/         # GameManager, SceneManager, AudioManager gibi tekil yöneticiler
│   ├── Player/            # Oyuncu davranış scriptleri
│   ├── Enemy/             # Düşman AI / davranış scriptleri
│   ├── UI/                # UI kontrolcüleri
│   ├── Systems/           # Bağımsız oyun sistemleri (envanter, quest, save/load vb.)
│   ├── Utilities/         # Yardımcı sınıflar, extension method'lar
│   └── Data/              # ScriptableObject tanımları, veri modelleri
├── Settings/             # URP Renderer / Pipeline Asset, Volume Profile ayarları
├── Sprites/              # 2D sprite'lar
├── Textures/             # Texture dosyaları
├── UI/                   # UI Toolkit / UI Document kaynakları
├── Addressables/         # Addressable Asset System içerikleri
└── StreamingAssets/      # Derleme sırasında olduğu gibi kopyalanan dosyalar
```

### Klasör Açıklamaları

| Klasör | Amaç |
|---|---|
| `Animations` | Tüm Animator Controller ve Animation Clip varlıkları |
| `Audio` | Müzik ve ses efektleri, alt klasörlere ayrılmış |
| `Materials` | Sahne ve nesnelerde kullanılan materyaller |
| `Models` | Dışarıdan içe aktarılan 3D modeller |
| `Prefabs` | Kategoriye göre ayrılmış prefab'lar |
| `Resources` | Sadece runtime dinamik yükleme gerektiren varlıklar (aşırı kullanılmamalı, bkz. Performans) |
| `Scenes` | Oyun sahneleri |
| `Scripts` | Katman/sorumluluğa göre ayrılmış C# kodları |
| `Settings` | URP ve grafik ayarları |
| `Sprites` / `Textures` | 2D görsel varlıklar |
| `UI` | UI Toolkit belgeleri, stil dosyaları |
| `Addressables` | Addressables sistemi ile yönetilen gruplar |
| `StreamingAssets` | Platforma göre değişmeden kopyalanması gereken ham dosyalar |

### Kurulum

1. Unity Hub üzerinden **Unity 6000.3.19f1** (veya üzeri aynı LTS hattı) sürümünü yükleyin, Android Build Support modülünü (SDK & NDK Tools, OpenJDK dahil) ekleyin.
2. Projeyi Unity Hub'a `Add` ile ekleyip açın.
3. İlk açılışta Package Manager paketleri indirecektir (Cinemachine dahil). İnternet bağlantısı gereklidir.
4. `Window > TextMeshPro > Import TMP Essential Resources` adımını bir kez çalıştırın (TMP artık `com.unity.ugui` paketine dahildir ancak Essentials ayrıca içe aktarılmalıdır).
5. `File > Build Settings > Android` sekmesinden `Switch Platform` yapın.

### Git Kullanım Kuralları

- `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `obj/`, build çıktıları asla commit edilmez (`.gitignore` içinde tanımlı).
- Asset Serialization **Force Text** ve Version Control modu **Visible Meta Files** olarak ayarlı kalmalıdır; bu sayede `.meta` dosyaları diff alınabilir kalır ve merge çakışmaları daha kolay çözülür.
- Sahne ve prefab dosyalarında birden fazla kişi aynı anda çalışmamalıdır — mümkünse sahneleri modüler tutup (additive scenes) çakışmayı azaltın.
- Her `.meta` dosyası ilgili asset ile birlikte commit edilmelidir; asla ayrı bırakılmamalıdır.
- Yeni bir Unity paketi eklendiğinde `Packages/manifest.json` ve `Packages/packages-lock.json` birlikte commit edilmelidir.

### Branch Stratejisi

- `main` — her zaman yayınlanabilir, stabil durum.
- `develop` — aktif geliştirmenin birleştiği ana entegrasyon dalı.
- `feature/<kısa-açıklama>` — yeni özellik geliştirme (örn. `feature/player-movement`).
- `bugfix/<kısa-açıklama>` — `develop` üzerindeki hata düzeltmeleri.
- `hotfix/<kısa-açıklama>` — `main` üzerinde acil prodüksiyon düzeltmeleri.
- `release/<versiyon>` — yayın öncesi stabilizasyon (örn. `release/0.2.0`).

Akış: `feature/*` → PR → `develop` → `release/*` → `main`.

### Commit İsimlendirme

[Conventional Commits](https://www.conventionalcommits.org/) formatı kullanılır:

```
<tip>(<kapsam>): <kısa açıklama>
```

Örnekler:

- `feat(player): çift zıplama mekaniği eklendi`
- `fix(ui): ana menüde buton tıklama gecikmesi giderildi`
- `refactor(systems): envanter sistemi SOLID prensiplerine göre yeniden yapılandırıldı`
- `perf(rendering): mobilde draw call sayısı azaltıldı`
- `chore(project): klasör yapısı ve proje ayarları düzenlendi`
- `docs(readme): kurulum adımları güncellendi`

Kullanılan tipler: `feat`, `fix`, `refactor`, `perf`, `chore`, `docs`, `test`, `style`.

### Kod Standartları

Ayrıntılar için [`CODING_STANDARDS.md`](CODING_STANDARDS.md) dosyasına bakın. Özet:

- Sınıf, metot ve public üyeler: `PascalCase`
- Private alanlar: `_camelCase`
- Public alanlar yerine `[SerializeField] private` + property tercih edilir
- Genel API'lerde XML doc yorumları (`/// <summary>`) kullanılır
- Büyük sınıflarda mantıksal bölümler `#region` ile ayrılır
- Mimari SOLID prensiplerine uyar; `Managers`, `Systems`, `Data` katmanları arasında net sorumluluk ayrımı korunur

### Mimari

Manager yapıları (Game/Audio/Pool/Save/UI Manager), Event Bus ve bunların bağımlılık grafiği için [`ARCHITECTURE.md`](ARCHITECTURE.md) dosyasına bakın. Özet: tüm manager'lar bir `Bootstrapper` (composition root) tarafından kurulur ve bir `ServiceLocator` üzerinden arayüzleriyle erişilir; gameplay/UI kodu manager'ları doğrudan çağırmak yerine Event Bus üzerinden haberleşir.

### Lisans

MIT — bkz. [LICENSE](./LICENSE).
