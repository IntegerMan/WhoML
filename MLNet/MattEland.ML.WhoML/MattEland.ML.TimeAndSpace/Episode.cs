using Microsoft.ML.Data;

public class Episode
{
    [LoadColumn(0)]
    public string EpisodeId { get; set; }

    [LoadColumn(1)]
    public string Title { get; set; }

    [LoadColumn(2)]
    public float Season { get; set; }

    [LoadColumn(3)]
    public string DoctorId { get; set; }

    [LoadColumn(4)]
    public bool IsSpecial { get; set; }

    [LoadColumn(5)]
    public bool IsEarth { get; set; }

    [LoadColumn(6)]
    public bool IsSpace { get; set; }

    [LoadColumn(7)]
    public bool IsPast { get; set; }

    [LoadColumn(8)]
    public bool IsPresent { get; set; }

    [LoadColumn(9)]
    public bool IsFuture { get; set; }

    [LoadColumn(10)]
    public bool IsOutsideTime { get; set; }

    [LoadColumn(11)]
    public string Producer { get; set; }

    [LoadColumn(12)]
    public string Director { get; set; }

    [LoadColumn(13)]
    public string Writer { get; set; }

    [LoadColumn(14)]
    public string Music { get; set; }

    [LoadColumn(15)]
    public string Date { get; set; } // Note: DateTime columns are not supported

    [LoadColumn(16)]
    public bool AiredFriday { get; set; }

    [LoadColumn(17)]
    public bool AiredMonday { get; set; }

    [LoadColumn(18)]
    public bool AiredSaturday { get; set; }

    [LoadColumn(19)]
    public bool AiredSunday { get; set; }

    [LoadColumn(20)]
    public bool AiredTuesday { get; set; }

    [LoadColumn(21)]
    public bool AiredWednesday { get; set; }

    [LoadColumn(22)]
    public float Rating { get; set; }

    [LoadColumn(23)]
    public float Views { get; set; }

    [LoadColumn(24)]
    public bool Has10 { get; set; }

    [LoadColumn(25)]
    public bool Has11 { get; set; }

    [LoadColumn(26)]
    public bool Has12 { get; set; }

    [LoadColumn(27)]
    public bool Has13 { get; set; }

    [LoadColumn(28)]
    public bool Has9 { get; set; }

    [LoadColumn(29)]
    public bool HasAmy { get; set; }

    [LoadColumn(30)]
    public bool HasBill { get; set; }

    [LoadColumn(31)]
    public bool HasClara { get; set; }

    [LoadColumn(32)]
    public bool HasCybermen { get; set; }

    [LoadColumn(33)]
    public bool HasDalek { get; set; }

    [LoadColumn(34)]
    public bool HasDanny { get; set; }

    [LoadColumn(35)]
    public bool HasDonna { get; set; }

    [LoadColumn(36)]
    public bool HasGrace { get; set; }

    [LoadColumn(37)]
    public bool HasGraham { get; set; }

    [LoadColumn(38)]
    public bool HasJackie { get; set; }

    [LoadColumn(39)]
    public bool HasJenny { get; set; }

    [LoadColumn(40)]
    public bool HasJudoon { get; set; }

    [LoadColumn(41)]
    public bool HasKate { get; set; }

    [LoadColumn(42)]
    public bool HasMadameKovorian { get; set; }

    [LoadColumn(43)]
    public bool HasMadameVastra { get; set; }

    [LoadColumn(44)]
    public bool HasMartha { get; set; }

    [LoadColumn(45)]
    public bool HasMickey { get; set; }

    [LoadColumn(46)]
    public bool HasNardole { get; set; }

    [LoadColumn(47)]
    public bool HasOod { get; set; }

    [LoadColumn(48)]
    public bool HasOsgood { get; set; }

    [LoadColumn(49)]
    public bool HasRiver { get; set; }

    [LoadColumn(50)]
    public bool HasRory { get; set; }

    [LoadColumn(51)]
    public bool HasRose { get; set; }

    [LoadColumn(52)]
    public bool HasRyan { get; set; }

    [LoadColumn(53)]
    public bool HasSarah { get; set; }

    [LoadColumn(54)]
    public bool HasSontaran { get; set; }

    [LoadColumn(55)]
    public bool HasSophie { get; set; }

    [LoadColumn(56)]
    public bool HasTheMaster { get; set; }

    [LoadColumn(57)]
    public bool HasTheSilent { get; set; }

    [LoadColumn(58)]
    public bool HasWarDoctor { get; set; }

    [LoadColumn(59)]
    public bool HasWeepingAngels { get; set; }

    [LoadColumn(60)]
    public bool HasChurchill { get; set; }

    [LoadColumn(61)]
    public bool HasYasmine { get; set; }

    [LoadColumn(62)]
    public bool HasZygon { get; set; }

}