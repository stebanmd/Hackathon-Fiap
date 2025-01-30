using Hackathon.Fiap.Core.ContributorAggregate;

namespace Hackathon.Fiap.UseCases.Contributors.Update;

public class UpdateContributorHandler(IRepository<Contributor> _repository)
  : ICommandHandler<UpdateContributorCommand, Result<ContributorDto>>
{
    public async Task<Result<ContributorDto>> Handle(UpdateContributorCommand request, CancellationToken cancellationToken)
    {
        var existingContributor = await _repository.GetByIdAsync(request.ContributorId, cancellationToken);
        if (existingContributor == null)
        {
            return Result.NotFound();
        }

        existingContributor.UpdateName(request.NewName!);

        await _repository.UpdateAsync(existingContributor, cancellationToken);

        return new ContributorDto(existingContributor.Id,
          existingContributor.Name, existingContributor.PhoneNumber?.Number ?? "");
    }
}
