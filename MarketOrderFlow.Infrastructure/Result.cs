namespace MarketOrderFlow.Infrastructure;

public class Result
{
    // Sonucun başarılı olup olmadığını belirtir
    public bool IsSuccess { get; private set; }

    // Hata mesajını saklar (başarı durumunda null olur)
    public string? ErrorMessage { get; private set; }

    // Ek verileri (isteğe bağlı) taşıyabilir
    public object? Data { get; private set; }

    // Başarılı sonuç döndürmek için
    public static Result Success() => new Result { IsSuccess = true };

    // Başarılı sonuç döndürmek ve ek veri sağlamak için
    public static Result Success(object data) => new Result { IsSuccess = true, Data = data };

    // Başarısız sonuç döndürmek için (hata mesajı ile)
    public static Result Failed(string errorMessage) => new Result { IsSuccess = false, ErrorMessage = errorMessage };

    // Başarısız sonuç döndürmek için (genel bir hata durumu)
    public static Result Failed() => new Result { IsSuccess = false };
}

