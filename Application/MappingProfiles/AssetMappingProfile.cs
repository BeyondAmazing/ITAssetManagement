using Application.Features.Assets.Commands.Create;
using Application.Features.Assets.Commands.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

class AssetMappingProfile : Profile
{
    public AssetMappingProfile()
    {
        // Map CreateAssetCommand to Asset
        CreateMap<CreateAssetCommand, Asset>()
        .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id since it's set by the database

        // Map UpdateAssetCommand to Asset
        CreateMap<UpdateAssetCommand, Asset>();
    }
}
