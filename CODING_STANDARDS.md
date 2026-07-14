# Kod Yazma Standartları

Bu doküman, proje genelinde tutarlı ve profesyonel C# kodu yazmak için uyulması gereken kuralları tanımlar.

## İsimlendirme

| Öğe | Kural | Örnek |
|---|---|---|
| Sınıf / Struct / Enum | `PascalCase` | `PlayerController`, `EnemyState` |
| Metot | `PascalCase` | `TakeDamage()`, `CalculateVelocity()` |
| Public / property | `PascalCase` | `CurrentHealth`, `IsGrounded` |
| Private alan | `_camelCase` | `_currentHealth`, `_rigidbody` |
| Statik private alan | `s_camelCase` | `s_instance` |
| Parametre / yerel değişken | `camelCase` | `damageAmount`, `targetPosition` |
| Sabit (`const`) | `PascalCase` | `MaxHealth` |
| Arayüz | `I` öneki + `PascalCase` | `IDamageable`, `IInteractable` |
| Namespace | `PascalCase`, proje kökünden | `GameMobil.Player`, `GameMobil.Systems` |
| Dosya adı | Sınıf adıyla birebir aynı | `PlayerController.cs` |

## Alanlar ve SerializeField

- Inspector'da düzenlenmesi gereken alanlar `[SerializeField] private` olarak tanımlanır, `public` alan kullanılmaz.
- Dışarıya sadece gerekliyse property üzerinden salt-okunur erişim sağlanır.

```csharp
[SerializeField] private float _moveSpeed = 5f;
[SerializeField] private Rigidbody _rigidbody;

public float MoveSpeed => _moveSpeed;
```

- `[Header]`, `[Tooltip]` ve `[Range]` attribute'ları Inspector okunabilirliği için aktif kullanılır.

## XML Documentation

Public API yüzeyine sahip her sınıf ve metot XML doc yorumu içermelidir:

```csharp
/// <summary>
/// Oyuncunun aldığı hasarı işler ve can değerini günceller.
/// </summary>
/// <param name="amount">Uygulanacak hasar miktarı.</param>
public void TakeDamage(float amount)
{
    // ...
}
```

## #region Kullanımı

Büyük sınıflarda ilgili üyeler mantıksal bloklara ayrılır:

```csharp
public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _moveSpeed;
    #endregion

    #region Unity Lifecycle
    private void Awake() { }
    private void Update() { }
    #endregion

    #region Public API
    public void Jump() { }
    #endregion

    #region Private Helpers
    private void ApplyGravity() { }
    #endregion
}
```

Küçük, tek sorumluluklu sınıflarda (ör. bir `ScriptableObject` veri sınıfı) `#region` zorunlu değildir.

## Mimari ve SOLID

- **Single Responsibility** — Her sınıf tek bir işten sorumlu olmalı. `Managers/` katmanı akış/koordinasyon, `Systems/` katmanı bağımsız oyun mantığı, `Data/` katmanı salt veri (ScriptableObject) barındırır.
- **Open/Closed** — Yeni davranışlar mevcut sınıfları değiştirmeden arayüz/soyutlama üzerinden eklenir (ör. `IDamageable`, `IInteractable`).
- **Liskov Substitution** — Alt sınıflar, üst sınıfın yerine sorunsuz geçebilmelidir; davranışı bozan override'lardan kaçının.
- **Interface Segregation** — Geniş, "her şeyi yapan" arayüzler yerine küçük, odaklı arayüzler tercih edilir.
- **Dependency Inversion** — Yüksek seviye sınıflar (ör. `GameManager`) somut sınıflara değil arayüzlere bağımlı olmalı; bağımlılıklar mümkün olduğunca dışarıdan enjekte edilir (constructor/`[SerializeField]` referans veya basit bir service locator).
- `Singleton` deseni yalnızca gerçekten tekil olması gereken yöneticiler için (ör. `GameManager`) kullanılır ve kötüye kullanılmaz.
- Sahne içi nesne referansları `FindObjectOfType` / `GameObject.Find` yerine `[SerializeField]` veya bir event/service sistemi ile sağlanır.

## Genel Kurallar

- Her dosyada tek bir public sınıf bulunur.
- `using` ifadeleri dosya başında, kullanılmayanlar bırakılmaz.
- Sihirli sayılar yerine adlandırılmış sabitler veya `[SerializeField]` alanlar kullanılır.
- Yorumlar "ne" yaptığını değil "neden" öyle yaptığını açıklar; kendini açıklayan koda gereksiz yorum eklenmez.
