namespace UniversityStudentSystem.Data.Models.CommonModels
{
    public interface IIdentifiableEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
