using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Xml.Linq;
using DAL;

namespace BLL
{
    public class TagService
    {
        IMapper mapper;
        public TagService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public bool AddTag(UnitOfWork unitOfWork, TagDto tagDto)
        {
            if (FindTagByName(unitOfWork, tagDto.Name) != null)
                throw new ValidationException("Такий тег вже існує");

            var tag = mapper.Map<Tag>(tagDto);
            unitOfWork.GetRepository<Tag>().Add(tag);
            unitOfWork.Save();
            return true;
        }

        public TagDto FindTagByName(UnitOfWork unitOfWork, string tagName)
        {
            return mapper.Map<TagDto>(unitOfWork.GetRepository<Tag>().Find(c => c.Name == tagName));
        }
        public List<TagDto> FindAllTags(UnitOfWork unitOfWork)
        {
            return mapper.Map<List<TagDto>>(unitOfWork.GetRepository<Tag>().GetAll());
        }

    }
}
