# Game_Mobil

Unity 6 (6000.3 LTS) URP tabanlı Android mobil oyun projesi.

## İçindekiler

- [Proje Yapısı](#proje-yapısı)
- [Klasör Açıklamaları](#klasör-açıklamaları)
- [Kurulum](#kurulum)
- [Git Kullanım Kuralları](#git-kullanım-kuralları)
- [Branch Stratejisi](#branch-stratejisi)
- [Commit İsimlendirme](#commit-isimlendirme)
- [Kod Standartları](#kod-standartları)
- [Mimari](#mimari)

## Proje Yapısı

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

## Klasör Açıklamaları

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

## Kurulum

1. Unity Hub üzerinden **Unity 6000.3.19f1** (veya üzeri aynı LTS hattı) sürümünü yükleyin, Android Build Support modülünü (SDK & NDK Tools, OpenJDK dahil) ekleyin.
2. Projeyi Unity Hub'a `Add` ile ekleyip açın.
3. İlk açılışta Package Manager paketleri indirecektir (Cinemachine dahil). İnternet bağlantısı gereklidir.
4. `Window > TextMeshPro > Import TMP Essential Resources` adımını bir kez çalıştırın (TMP artık `com.unity.ugui` paketine dahildir ancak Essentials ayrıca içe aktarılmalıdır).
5. `File > Build Settings > Android` sekmesinden `Switch Platform` yapın.

## Git Kullanım Kuralları

- `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `obj/`, build çıktıları asla commit edilmez (`.gitignore` içinde tanımlı).
- Asset Serialization **Force Text** ve Version Control modu **Visible Meta Files** olarak ayarlı kalmalıdır; bu sayede `.meta` dosyaları diff alınabilir kalır ve merge çakışmaları daha kolay çözülür.
- Sahne ve prefab dosyalarında birden fazla kişi aynı anda çalışmamalıdır — mümkünse sahneleri modüler tutup (additive scenes) çakışmayı azaltın.
- Her `.meta` dosyası ilgili asset ile birlikte commit edilmelidir; asla ayrı bırakılmamalıdır.
- Yeni bir Unity paketi eklendiğinde `Packages/manifest.json` ve `Packages/packages-lock.json` birlikte commit edilmelidir.

## Branch Stratejisi

- `main` — her zaman yayınlanabilir, stabil durum.
- `develop` — aktif geliştirmenin birleştiği ana entegrasyon dalı.
- `feature/<kısa-açıklama>` — yeni özellik geliştirme (örn. `feature/player-movement`).
- `bugfix/<kısa-açıklama>` — `develop` üzerindeki hata düzeltmeleri.
- `hotfix/<kısa-açıklama>` — `main` üzerinde acil prodüksiyon düzeltmeleri.
- `release/<versiyon>` — yayın öncesi stabilizasyon (örn. `release/0.2.0`).

Akış: `feature/*` → PR → `develop` → `release/*` → `main`.

## Commit İsimlendirme

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

## Kod Standartları

Ayrıntılar için [`CODING_STANDARDS.md`](CODING_STANDARDS.md) dosyasına bakın. Özet:

- Sınıf, metot ve public üyeler: `PascalCase`
- Private alanlar: `_camelCase`
- Public alanlar yerine `[SerializeField] private` + property tercih edilir
- Genel API'lerde XML doc yorumları (`/// <summary>`) kullanılır
- Büyük sınıflarda mantıksal bölümler `#region` ile ayrılır
- Mimari SOLID prensiplerine uyar; `Managers`, `Systems`, `Data` katmanları arasında net sorumluluk ayrımı korunur

## Mimari

Manager yapıları (Game/Audio/Pool/Save/UI Manager), Event Bus ve bunların bağımlılık grafiği için [`ARCHITECTURE.md`](ARCHITECTURE.md) dosyasına bakın. Özet: tüm manager'lar bir `Bootstrapper` (composition root) tarafından kurulur ve bir `ServiceLocator` üzerinden arayüzleriyle erişilir; gameplay/UI kodu manager'ları doğrudan çağırmak yerine Event Bus üzerinden haberleşir.
