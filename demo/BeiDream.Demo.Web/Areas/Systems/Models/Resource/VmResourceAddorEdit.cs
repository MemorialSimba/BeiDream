﻿using System;
using BeiDream.AutoMapper;
using BeiDream.Demo.Domain.Enums;
using BeiDream.Demo.Service.Resources.Dtos;

namespace BeiDream.Demo.Web.Areas.Systems.Models.Resource
{
    [AutoMapFrom(typeof(ResourceDto))]
    [AutoMapTo(typeof(ResourceDto))]
    public class VmResourceAddorEdit
    {
        public VmResourceAddorEdit()
        {
        }
        public VmResourceAddorEdit(string parentId)
        {
            if (!string.IsNullOrWhiteSpace(parentId))
                ParentId = new Guid(parentId);
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int? SortId { get; set; }
        public string Uri { get; set; }
        public string Type { get; set; }
        public bool Enabled { get; set; }
        public byte[] Version { get; set; }
    }
}