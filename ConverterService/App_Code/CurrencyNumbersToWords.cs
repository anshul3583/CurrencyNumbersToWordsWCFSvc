using System.Runtime.Serialization;

[DataContract]
public class CurrencyNumbersToWords
{
    [DataMember]
    public string CurrencyNumber { get; set; }

    [DataMember]
    public string Result { get; set; }
}