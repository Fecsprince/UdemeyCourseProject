﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.SQL;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory> Catecontext;

        public ProductManagerController(IRepository<Product> Prodcontext, IRepository<ProductCategory> ProdCatecontext)
        {
            context = Prodcontext;
            Catecontext = ProdCatecontext;
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product viewModel = new Product();

            //viewModel.Product = new Product();
            //viewModel.ProductCates = Catecontext.Collection();

            ViewBag.Category = new SelectList(Catecontext.Collection(), "Name", "Id");
            return View(viewModel);
            //Product prod = new Product();
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod)
        {
            if (!ModelState.IsValid)
            {
                return View(prod);
            }
            else
            {
                
                context.Insert(prod);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            var prod = context.Find(Id);

            if (prod == null)
            {
                return HttpNotFound();
            }
            else
            {
               // Product viewModel = new Product();
                //viewModel.Product = new Product();
                //viewModel.ProductCates = Catecontext.Collection();
                ViewBag.Category = new SelectList(Catecontext.Collection(), "Name", "Id");
                return View(prod);

            //    ViewBag.Id = new SelectList(Catecontext.Collection(), "Id", "Name");
            //    return View(prod);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p, string Id)
        {
            var prodToEdit = context.Find(Id);

            if (prodToEdit != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(p);

                }

                else
                {
                    prodToEdit.Name = p.Name;
                    prodToEdit.Category = p.Category;
                    prodToEdit.Image = p.Image;
                    prodToEdit.Price = p.Price;
                    prodToEdit.Description = p.Description;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }

            else
            {
                return HttpNotFound();
            }
        }


        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfrimDelete(string Id)
        {
            
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }

}