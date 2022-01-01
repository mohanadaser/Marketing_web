using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class menucategory : ViewComponent
{

    private MyDbcontext _context;



    public menucategory(MyDbcontext context)
    {
        _context = context;


    }

    public IViewComponentResult Invoke()
    {
        var tbl = _context.productTypes.ToList();

        return View("/views/shared/_menucategory.cshtml", tbl);

    }



}

