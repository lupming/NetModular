﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.ModuleService.ViewModels
{
    public class ModuleOptionsUpdateModel
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [Required(ErrorMessage = "请选择模块")]
        public string Code { get; set; }

        /// <summary>
        /// 配置模型实例
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public Dictionary<string, object> Values { get; set; }
    }
}
