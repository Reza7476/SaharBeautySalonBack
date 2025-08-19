using BeautySalon.Application.Treatments.Contracts.Dto;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.Treatments.Contracts;
public interface TreatmentHandler : IScope
{
    Task<long> Add(AddTreatmentHandlerDto dto);
}
