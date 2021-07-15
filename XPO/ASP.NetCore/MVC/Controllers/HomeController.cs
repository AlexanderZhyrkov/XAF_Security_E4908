﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using Microsoft.AspNetCore.Authorization;
using BusinessObjectsLibrary;

namespace MvcApplication.Controllers {
	[Authorize]
	public class HomeController : Microsoft.AspNetCore.Mvc.Controller {
		SecurityProvider securityProvider;
		public HomeController(SecurityProvider securityProvider){
			this.securityProvider = securityProvider;
		}
		public IActionResult Index() {
			using(IObjectSpace objectSpace = securityProvider.ObjectSpaceProvider.CreateObjectSpace()) {
				ITypeInfo typeInfo = objectSpace.TypesInfo.PersistentTypes.FirstOrDefault(t => t.Name == typeof(Employee).Name);
				PermissionHelper permissionHelper = new PermissionHelper(securityProvider.Security);
				TypePermission typePermission = permissionHelper.CreateTypePermission(typeInfo);
				return View(typePermission);
			}
		}
		protected override void Dispose(bool disposing) {
			if(disposing) {
				securityProvider?.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
