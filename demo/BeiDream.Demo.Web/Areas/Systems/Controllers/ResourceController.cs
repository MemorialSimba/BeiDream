﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.AutoMapper;
using BeiDream.Demo.Domain.Queries;
using BeiDream.Demo.Service.Contracts;
using BeiDream.Demo.Web.Areas.Systems.Models.Resource;
using BeiDream.Utils.Reflection;
using BeiDream.Web.Mvc.EasyUi;
using BeiDream.Web.Mvc.EasyUi.Tree;
using Castle.Components.DictionaryAdapter;

namespace BeiDream.Demo.Web.Areas.Systems.Controllers
{
    public class ResourceController : EasyUiControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        // GET: Systems/Resource
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Query(ResourceQuery query)
        {
            SetPage(query);
            var result = _resourceService.Query(query).Convert(p => p.ToTreeGridVm());
            return ToDataTreeGridResult(result, false, result.TotalCount);
        }
        public PartialViewResult Add(string id)
        {
            return PartialView("Parts/Form", new VmResourceAddorEdit(id));
        }
        public PartialViewResult Edit(Guid id)
        {
            var dto = _resourceService.Find(id);
            return PartialView("Parts/Form", dto.MapTo<VmResourceAddorEdit>());
        }

        public ActionResult Save(VmResourceAddorEdit vm)
        {
            return AjaxOkResponse("保存成功！");
        }
        [HttpPost]
        public ActionResult Delete(string ids)
        {
            //_resourceService.Delete(new Guid(ids));
            return AjaxOkResponse("删除成功！");
        }
        public ActionResult GetResources()
        {
            var list = _resourceService.QueryAll();
            List<VmResourceTreeGrid> dtos = list.Select(item => item.MapTo<VmResourceTreeGrid>()).ToList();
            return ToJsonResult(new EasyUiTreeData(dtos).GetNodes());
        }
        public ActionResult GetResourceTypes()
        {
            //todo，设计成通用的，传入枚举类型，自动生成下拉列表模式
            List<EasyUiCombobox> list=new List<EasyUiCombobox>();
            list.Add(new EasyUiCombobox() { value = "Module", text = "模块", group = "" });
            list.Add(new EasyUiCombobox() { value = "Menu", text = "菜单", group = "" });
            list.Add(new EasyUiCombobox() { value = "Operation", text = "操作(按钮)", group = "" });
            return ToJsonResult(list);
        }
    }
}