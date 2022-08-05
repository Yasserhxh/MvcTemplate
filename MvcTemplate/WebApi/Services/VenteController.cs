using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenteController : Controller
    {
        private readonly IProduitVendableService produitVendableService;
        private readonly IPointVenteService pointVenteService;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IFamilleProduitService familleProduitService;
        private readonly IVenteProduitsService venteProduitsService;

        public VenteController(IProduitVendableService produitVendableService, IPointVenteService pointVenteService, IAuthentificationRepository authentificationRepository, IVenteProduitsService venteProduitsService, IFamilleProduitService familleProduitService)
        {
            this.produitVendableService = produitVendableService;
            this.familleProduitService = familleProduitService;
            this.pointVenteService = pointVenteService;
            this.authentificationRepository = authentificationRepository;
            this.venteProduitsService = venteProduitsService;

        }
        [HttpGet("GetALL/{userId}", Name = "GetALL")]
        public JsonResult GetALL( string userId)
        {
            var id = authentificationRepository.GetUserAboIdById(userId);
            var pdv = authentificationRepository.Geturser(userId).Position_Vente.Point_Vente.PointVente_Id;
            var categ = familleProduitService.getListFamilles((int)id);
            var all = new List<SyncroApiModel>();
            foreach (var f in categ)
            {
                var syncro = new SyncroApiModel()
                {
                    FamilleProduit_AbonnemnetId = f.FamilleProduit_AbonnemnetId,
                    FamilleProduit_Id = f.FamilleProduit_Id,
                    FamilleProduit_Image = f.FamilleProduit_Image,
                    FamilleProduit_Libelle = f.FamilleProduit_Libelle
                };
                foreach(var s in f.sousFamille)
                {
                   
                    var model = new List<VenteApiModel>();
                    var stocks = pointVenteService.getStockPointVente(id, pdv).Where(p => p.Produit_Vendable.Sous_Famille.SousFamille_ID == s.SousFamille_ID);
                    if (stocks.Count() > 1)
                    {
                        var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation, tva = p.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.Unite_Mesure });
                        foreach (var ss in stock)
                        {
                            var formesAll = stocks.Where(p => p.PointVenteStock_ProduitID == ss.Key.key); // pointVenteService.getStockPointVente(id, pdv).Where(p => p.PointVenteStock_ProduitID == ss.Key.key);
                            var formes = formesAll.Select(p => p.Forme_Produit);
                            var i = 0;
                            var formeApi = new List<FormeApiResultModel>();
                            foreach (var item in formes)
                            {
                                //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                                var m1 = new FormeApiResultModel
                                {
                                    Forme_ID = item.FormeProduit_ID,
                                    Forme_ProduitVendableId = item.FormeProduit_ProduitID,
                                    Forme_CoutDeRevient = item.FormeProduit_CoutDeRevient,
                                    Forme_Libelle = item.FormeProduit_Libelle,
                                    Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                                    //ListPrix = prix,
                                    Forme_PrixVente = item.FormeProduit_PrixVente

                                };
                                formeApi.Add(m1);
                                i++;
                            }
                            var v1 = new VenteApiModel
                            {
                                ID = ss.Key.key,
                                Designation = ss.Key.item,
                                Image = ss.Key.photo,
                                Type = "vendable",
                                Unite = ss.Key.unite,
                                tva = ss.Key.tva,
                                formes = formeApi
                            };
                            model.Add(v1);
                        }

                    }
                    else
                    {
                        foreach (var ss in stocks)
                        {
                            var formesAll = stocks.Where(p => p.PointVenteStock_ProduitID == ss.PointVenteStock_ProduitID); // pointVenteService.getStockPointVente(id, pdv).Where(p => p.PointVenteStock_ProduitID == ss.Key.key);
                            var formes = formesAll.Select(p => p.Forme_Produit);
                            var i = 0;
                            var formeApi = new List<FormeApiResultModel>();
                            foreach (var item in formes)
                            {
                                //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                                var m1 = new FormeApiResultModel
                                {
                                    Forme_ID = item.FormeProduit_ID,
                                    Forme_ProduitVendableId = item.FormeProduit_ProduitID,
                                    Forme_CoutDeRevient = item.FormeProduit_CoutDeRevient,
                                    Forme_Libelle = item.FormeProduit_Libelle,
                                    Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                                    //ListPrix = prix,
                                    Forme_PrixVente = item.FormeProduit_PrixVente

                                };
                                formeApi.Add(m1);
                                i++;
                            }
                            var v1 = new VenteApiModel
                            {
                                ID = ss.PointVenteStock_ProduitID,
                                Designation = ss.Produit_Vendable.ProduitVendable_Designation,
                                Image = ss.Produit_Vendable.ProduitVendable_GrandePhoto,
                                Type = "vendable",
                                Unite = ss.Produit_Vendable.Unite_Mesure,
                                tva = ss.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage,
                                formes = formeApi
                            };
                            model.Add(v1);
                        }

                    }
                    var pp = pointVenteService.getStockProduitPackage(id, pdv).Where(p => p.formes.Count() > 0 && p.ProduitPackage_FamilleID == s.SousFamille_ID);
                    var package = pp.Where(p => p.formes.Where(x => x.FormeProduit_PrixVente > 0).Count() > 0);
                    var prt = produitVendableService.getListProduitConsomableEnMagasin(id, pdv, s.SousFamille_ID);
                    var pret = prt;
                    foreach (var pack in package)
                    {
                        var formesAll = stocks.Where(p => p.PointVenteStock_ProduitID == pack.ProduitPackage_ID); //pointVenteService.getStockPointVente(id, pdv).Where(p => p.PointVenteStock_ProduitID == pack.ProduitPackage_ID);
                        var formes = formesAll.Select(p => p.Forme_Produit);
                        var i = 0;
                        var formeApi = new List<FormeApiResultModel>();
                        foreach (var item in formes)
                        {
                            //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                            var m1 = new FormeApiResultModel
                            {
                                Forme_ID = item.FormeProduit_ID,
                                Forme_ProduitVendableId = item.FormeProduit_ProduitID,
                                Forme_CoutDeRevient = item.FormeProduit_CoutDeRevient,
                                Forme_Libelle = item.FormeProduit_Libelle,
                                Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                                //ListPrix = prix,
                                Forme_PrixVente = item.FormeProduit_PrixVente

                            };
                            formeApi.Add(m1);
                            i++;
                        }
                        var v1 = new VenteApiModel
                        {
                            ID = pack.ProduitPackage_ID,
                            Designation = pack.ProduitPackage_Designation,
                            Image = pack.ProduitPackage_Photo,
                            Type = "package",
                            Unite = pack.Unite_Mesure,
                            formes = formeApi

                        };
                        model.Add(v1);
                    }
                    foreach (var p in pret)
                    {
                        var formesAll = stocks.Where(x => x.PointVenteStock_ProduitID == p.ProduitPretConsomer_ID); //pointVenteService.getStockPointVente(id, pdv).Where(x => x.PointVenteStock_ProduitID == p.ProduitPretConsomer_ID);
                        var formes = formesAll.Select(x => x.Forme_Produit);
                        var i = 0;
                        var formeApi = new List<FormeApiResultModel>();
                        foreach (var item in formes)
                        {
                            //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                            var m1 = new FormeApiResultModel
                            {
                                Forme_ID = item.FormeProduit_ID,
                                Forme_ProduitVendableId = item.FormeProduit_ProduitID,
                                Forme_CoutDeRevient = item.FormeProduit_CoutDeRevient,
                                Forme_Libelle = item.FormeProduit_Libelle,
                                Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                                //ListPrix = prix,
                                Forme_PrixVente = item.FormeProduit_PrixVente

                            };
                            formeApi.Add(m1);
                            i++;
                        }
                        var v1 = new VenteApiModel
                        {
                            ID = p.ProduitPretConsomer_ID,
                            Designation = p.ProduitPretConsomer_Designation,
                            Image = p.ProduitPretConsomer_Photo,
                            Type = "consomable",
                            Unite = p.Unite_Mesure,
                            formes = formeApi

                        };
                        model.Add(v1);
                    }
                    var sousFamille = new SousFamilleApi()
                    {
                        SousFamille_AbonnementID = s.SousFamille_AbonnementID,
                        SousFamille_ID = s.SousFamille_ID,
                        SousFamille_Image = s.SousFamille_Image,
                        SousFamille_Libelle = s.SousFamille_Libelle,
                        SousFamille_ParentID = s.SousFamille_ParentID,
                        produits = model
                    };
                    syncro.sousFamille.Add(sousFamille);
                }
                all.Add(syncro);
            }
            return new JsonResult(all);

        }

        // GET: api/Vente/Get
        [HttpPost]
        [Route("getProduitVendables")]
        public JsonResult GetProduitVendables([FromBody] FormeCategApi formApiBody)
        {            
            var id = authentificationRepository.GetUserAboIdById(formApiBody.userId);
            var pdv = authentificationRepository.Geturser(formApiBody.userId).Position_Vente.Point_Vente.PointVente_Id;
            var stocks = pointVenteService.getStockPointVente(id, pdv).Where(p=>p.Produit_Vendable.Sous_Famille.SousFamille_ID == formApiBody.sousCategorieID);
            var model = new List<VenteApiModel>();
            if (stocks.Count() > 1)
            {
                var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID,planif = p.Produit_Vendable.ProduitVendable_PlanificationFlag,souscateg = p.Produit_Vendable.ProduitVendable_FamilleProduitId, item = p.Produit_Vendable.ProduitVendable_Designation, tva = p.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.Unite_Mesure });
                foreach (var s in stock)
                {
                    var v1 = new VenteApiModel
                    {
                        ID = s.Key.key,
                        Designation = s.Key.item,
                        Image = s.Key.photo,
                        Type = "vendable",
                        Unite = s.Key.unite,
                        tva = s.Key.tva,
                        planifFlag = s.Key.planif,
                        sousCategId = s.Key.souscateg,
                    };
                    model.Add(v1);
                }
            }
            else
            {
                foreach (var s in stocks)
                {
                    var v1 = new VenteApiModel
                    {
                        ID = s.PointVenteStock_ProduitID,//s.Key.key,
                        Designation =s.Produit_Vendable.ProduitVendable_Designation, //s.Key.item,
                        Image = s.Produit_Vendable.ProduitVendable_GrandePhoto,//s.Key.photo,
                        Type = "vendable",
                        Unite = s.Produit_Vendable.Unite_Mesure,
                        tva =  s.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage,//s.Key.tva,
                        planifFlag = s.Produit_Vendable.ProduitVendable_PlanificationFlag,
                        sousCategId = s.Produit_Vendable.ProduitVendable_FamilleProduitId,
                    };
                    model.Add(v1);
                }
            }
            // var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable});
            var pp = pointVenteService.getStockProduitPackage(id, pdv).Where(p => p.formes.Count() > 0 && p.ProduitPackage_FamilleID == formApiBody.sousCategorieID);
            var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            var prt = produitVendableService.getListProduitConsomableEnMagasin(id,pdv,formApiBody.sousCategorieID);
            var pret = prt;
             
            foreach(var pack in package)
            {
                var v1 = new VenteApiModel
                {
                    ID = pack.ProduitPackage_ID,
                    Designation = pack.ProduitPackage_Designation,
                    Image = pack.ProduitPackage_Photo,
                    Type = "package",
                    Unite = pack.Unite_Mesure,
                    sousCategId = pack.ProduitPackage_FamilleID,
                    planifFlag = 1,

                };
                model.Add(v1);
            }
            foreach(var p in pret)
            {
                var v1 = new VenteApiModel
                {
                    ID = p.ProduitPretConsomer_ID,
                    Designation = p.ProduitPretConsomer_Designation,
                    Image = p.ProduitPretConsomer_Photo,
                    Type = "consomable",
                    Unite = p.Unite_Mesure,
                    sousCategId = p.ProduitPretConsomer_FamilleProduit,
                    planifFlag = 1,
                };
                model.Add(v1);
            }
            return new JsonResult(model);
        }
        [HttpPost]
        [Route("getProduitVendablesAll")]
        public JsonResult GetProduitVendablesAll([FromBody] FormeCategApi formApiBody)
        {            
            var id = authentificationRepository.GetUserAboIdById(formApiBody.userId);
            var pdv = authentificationRepository.Geturser(formApiBody.userId).Position_Vente.Point_Vente.PointVente_Id;
            var familles = familleProduitService.getListFamillesByPdv(id, pdv);
            var sousfamillesID = new List<int>();
            foreach(var f in familles)
            {
                sousfamillesID.AddRange(f.sousFamille.Select(p => p.SousFamille_ID));
            }

            var stocks = pointVenteService.getStockPointVente(id, pdv);
            var model = new List<VenteApiModel>();
            if (stocks.Count() > 1)
            {
                var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID,sousCateg = p.Produit_Vendable.ProduitVendable_FamilleProduitId,planif = p.Produit_Vendable.ProduitVendable_PlanificationFlag, item = p.Produit_Vendable.ProduitVendable_Designation, tva = p.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.ProduitVendable_UniteMesureId });
                foreach (var s in stock)
                {
                    if (sousfamillesID.Any(x => x == s.Key.sousCateg)) 
                    {
                        var v1 = new VenteApiModel
                        {
                            ID = s.Key.key,
                            Designation = s.Key.item,
                            Image = s.Key.photo,
                            Type = "vendable",
                            uniteId = s.Key.unite,
                            sousCategId = s.Key.sousCateg,
                            tva = s.Key.tva,
                            planifFlag = s.Key.planif,
                        };
                        model.Add(v1);
                    }
                    
                }
            }
            else
            {
                foreach (var s in stocks)
                {
                    if (sousfamillesID.Any(x => x == s.Produit_Vendable.ProduitVendable_FamilleProduitId))
                    {
                        var v1 = new VenteApiModel
                        {
                            ID = s.PointVenteStock_ProduitID,//s.Key.key,
                            Designation = s.Produit_Vendable.ProduitVendable_Designation, //s.Key.item,
                            Image = s.Produit_Vendable.ProduitVendable_GrandePhoto,//s.Key.photo,
                            Type = "vendable",
                            sousCategId = s.Produit_Vendable.ProduitVendable_FamilleProduitId,
                            uniteId = s.Produit_Vendable.ProduitVendable_UniteMesureId, //s.Key.unite,
                            tva = s.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage,//s.Key.tva,
                            planifFlag = s.Produit_Vendable.ProduitVendable_PlanificationFlag,
                        };
                        model.Add(v1);
                    }
                    
                }
            }
            // var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable});
            var pp = pointVenteService.getStockProduitPackage(id, pdv).Where(p => p.formes.Count() > 0);
            var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            var prt = produitVendableService.getListProduitConsomableEnMagasin(id,pdv,null);
            var pret = prt;
             
            foreach(var pack in package)
            {
                if (sousfamillesID.Any(x => x == pack.ProduitPackage_FamilleID))
                {
                    var v1 = new VenteApiModel
                    {
                        ID = pack.ProduitPackage_ID,
                        Designation = pack.ProduitPackage_Designation,
                        Image = pack.ProduitPackage_Photo,
                        Type = "package",
                        sousCategId = pack.ProduitPackage_FamilleID,
                        uniteId = pack.ProduitPackage_UniteVente,
                        planifFlag = 1,
                    };
                    model.Add(v1);
                }
                
            }
            foreach(var p in pret)
            {
                if (sousfamillesID.Any(x => x == p.ProduitPretConsomer_FamilleProduit))
                {
                    var v1 = new VenteApiModel
                    {
                        ID = p.ProduitPretConsomer_ID,
                        Designation = p.ProduitPretConsomer_Designation,
                        Image = p.ProduitPretConsomer_Photo,
                        Type = "consomable",
                        sousCategId = p.ProduitPretConsomer_FamilleProduit,
                        uniteId = p.ProduitPretConsomer_UniteMesureAchatId,
                        planifFlag = 1,
                    };
                    model.Add(v1);
                }
            }
            return new JsonResult(model);
        }

        [HttpPost]
        [Route("getFormes")]
        public JsonResult getFormes([FromBody] FormApiBody formApiBody)
        {
            var AboId = authentificationRepository.GetUserAboIdById(formApiBody.userId);
            var pdv = authentificationRepository.Geturser(formApiBody.userId).Position_Vente.Point_Vente.PointVente_Id;
            if (formApiBody.Type == "vendable")
            {
                var formesAll = pointVenteService.getStockPointVente(AboId, pdv).Where(p => p.PointVenteStock_ProduitID == formApiBody.Id);
                var formes = formesAll.Select(p => p.Forme_Produit);
                var i = 0;
                var model = new List<FormeApiResultModel>();
                foreach(var f in formes)
                {
                    //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitVendableId = f.FormeProduit_ProduitID,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        Forme_QteStock = (formesAll.Count() > 0  ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                        //ListPrix = prix,
                        Forme_PrixVente = f.FormeProduit_PrixVente
                    };
                    model.Add(m1);
                    i++;
                }
                return new JsonResult(model);
            }
            if (formApiBody.Type == "package")
            {
                var formes = produitVendableService.getListFormeProduitsPackage(AboId, formApiBody.Id);
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, pdv);
                    //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitPackageId = f.FormeProduit_ProduitPackageId,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        //ListPrix = prix,
                        Forme_PrixVente = f.FormeProduit_PrixVente,
                        Forme_QteStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0)

                    };
                    model.Add(m1);
                }
                return new JsonResult(model);
            } 
            else
            {
                var formes = produitVendableService.getListFormeProduitsPret(AboId, formApiBody.Id);
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                    var QteForme = produitVendableService.getListProduitsStockesPdv(AboId, f.FormeProduit_ID).FirstOrDefault();
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitPretConsomerId = f.FormeProduit_ProduitPretId,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        //ListPrix = prix,
                        Forme_PrixVente = f.FormeProduit_PrixVente,
                        Forme_QteStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0)
                    };
                    model.Add(m1);
                }
                return new JsonResult(model);
            }
        }
        [HttpPost]
        [Route("getFormesAll")]
        public JsonResult getFormesAll([FromBody] FormApiBody formApiBody)
        {
            var AboId = authentificationRepository.GetUserAboIdById(formApiBody.userId);
            var pdv = authentificationRepository.Geturser(formApiBody.userId).Position_Vente.Point_Vente.PointVente_Id;
            var formesAll = pointVenteService.getStockPointVente(AboId, pdv);
            var formesPret =  produitVendableService.getFormesConsomableEnMagasin(AboId,pdv);
            var formesPack = pointVenteService.getStockFormePackage(AboId, pdv);
            var formes = formesAll.Select(p => p.Forme_Produit);
            var i = 0;
            var model = new List<FormeApiResultModel>();
            foreach (var f in formes)
            {
                //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                var m1 = new FormeApiResultModel
                {
                    Forme_ID = f.FormeProduit_ID,
                    Forme_ProduitVendableId = f.FormeProduit_ProduitID,
                    Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                    Forme_Libelle = f.FormeProduit_Libelle,
                    Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                    Forme_PrixVente = f.FormeProduit_PrixVente,
                    type = "vendable",
                };
                model.Add(m1);
                i++;
            }
            foreach (var f in formesPret)
            {
                var QteForme = produitVendableService.getListProduitsStockesPdv(AboId, f.FormeProduit_ID).FirstOrDefault();
                var m1 = new FormeApiResultModel
                {
                    Forme_ID = f.FormeProduit_ID,
                    Forme_ProduitPretConsomerId = f.FormeProduit_ProduitPretId,
                    Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                    Forme_Libelle = f.FormeProduit_Libelle,
                    Forme_QteStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0),
                    Forme_PrixVente = f.FormeProduit_PrixVente,
                    type = "consomable",
                };
                model.Add(m1);
            }
            foreach (var f in formesPack)
            {
                var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, pdv);
                var m1 = new FormeApiResultModel
                {
                    Forme_ID = f.FormeProduit_ID,
                    Forme_ProduitPackageId = f.FormeProduit_ProduitPackageId,
                    Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                    Forme_Libelle = f.FormeProduit_Libelle,
                    Forme_QteStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0),
                    Forme_PrixVente = f.FormeProduit_PrixVente,
                    type = "package",
                };
                model.Add(m1);
            }
            return new JsonResult(model);
        }

        // POST: api/Vente/facturer
        [HttpPost]
        [Route("facturer")]
        public async Task<JsonResult> facturer([FromBody] FactureMobileCallModel factureMobileCall)
        {
            var aboId = authentificationRepository.GetUserAboIdById(factureMobileCall.userId);
            var posId = authentificationRepository.Geturser(factureMobileCall.userId).PositionVente_ID;
            var pdvId = authentificationRepository.Geturser(factureMobileCall.userId).Position_Vente.PositionVente_PointVenteId;
            if (factureMobileCall.isCommande == true)
            {
                List<CommandeDetailModel> details = new List<CommandeDetailModel>();
                foreach (var facture in factureMobileCall.factureMobiles)
                {
                    var coutRevient = produitVendableService.findFormulaireFormeProduit(facture.FormeProduitId).FormeProduit_CoutDeRevient;
                    CommandeDetailModel detail = new CommandeDetailModel()
                    {
                        CommandeDetail_Prix = facture.Prix,
                        CommandeDetail_Quantite = facture.Quantite,
                        CommandeDetail_UniteId = facture.UniteId,
                        CommandeDetail_FormeProduitId = facture.FormeProduitId,
                        CommandeDetail_CoutdeRevient = coutRevient * facture.Quantite,
                        CommandeDetail_Marge = facture.Prix - coutRevient,
                    };

                    details.Add(detail);
                }
                //For test purpose 
               // factureMobileCall.commande.dateLivraison = DateTime.Now;
                CommandeModel commande = new CommandeModel()
                {
                    Commande_AbonnementId = aboId,
                    Commande_PointVenteId = pdvId,
                    Commande_CaisseCommandeId = (int)posId,
                    Commande_UtilisateurCommandeId = factureMobileCall.userId,
                    Commande_DateCreation = DateTime.Now,
                    Commande_Date = DateTime.Now,
                    //Commande_MontantTotal = details.Sum(v => v.CommandeDetail_Prix),
                    Commande_MontantTotal = (decimal)factureMobileCall.totalRemise,
                    Commande_TauxdeRemise = (decimal)factureMobileCall.tauxRemise,
                    Commande_MontantSansRemise = (decimal)factureMobileCall.totalSansRemise,
                    
                    //Vente_ModePaiement = factureMobileCall.venteModePaimentId,
                    //Vente_Commentaire = factureMobileCall.venteCommentaire,
                    //Vente_Marge = details.Sum(v => v.VenteDetails_Marge),
                    Commande_Avance = factureMobileCall.commande.montantAvance,
                    Commande_DateLivraisonPrevue = factureMobileCall.commande.dateLivraison,
                    Commande_NomDemandeurs = factureMobileCall.commande.clientNom,
                    Commande_EtatDeLivraison = 1,
                    Commande_NumeroTicket = factureMobileCall.Vente_NumeroTicket,
                    Commande_NumeroTel = factureMobileCall.commande.clientTelephone,
                    details = details,
                    Tva = factureMobileCall.tvaList,
                };
                if (commande.Commande_Avance > 0 && commande.Commande_Avance < commande.Commande_MontantTotal)
                    commande.Commande_EtatDePaiement = 2;
                else if (commande.Commande_Avance == 0)
                    commande.Commande_EtatDePaiement = 3;
                else if (commande.Commande_Avance == commande.Commande_MontantTotal)
                    commande.Commande_EtatDePaiement = 1;
                return new JsonResult(await venteProduitsService.CreateCommade(commande));
            }
            else
            {
                if(factureMobileCall.isGratuite==true)
                {
                    List<GratuiteDetailsModel> details = new List<GratuiteDetailsModel>();
                    foreach (var facture in factureMobileCall.factureMobiles)
                    {
                        var coutRevient = produitVendableService.findFormulaireFormeProduit(facture.FormeProduitId).FormeProduit_CoutDeRevient;
                        GratuiteDetailsModel detail = new GratuiteDetailsModel()
                        {
                            GratuiteDetails_Quantite = facture.Quantite,
                            GratuiteDetails_UniteVenteID = facture.UniteId,
                            GratuiteDetails_FormeID = facture.FormeProduitId,
                            GratuiteDetails_CoutDeRevient = coutRevient * facture.Quantite ,
                            GratuiteDetails_DateCreation = DateTime.Now,
                        };

                        details.Add(detail);
                    }
                    GratuiteModel gratuite = new GratuiteModel()
                    {
                        Gratuite_AbonnementID = aboId,
                        Gratuite_PositionVente = (int)posId,
                        Gratuite_UtilisateurID = factureMobileCall.userId,
                        Gratuite_DateCreation = DateTime.Now,
                        Gratuite_CoutDeRevientTotal = details.Sum(v => v.GratuiteDetails_CoutDeRevient),
                        details = details,
                     //   Tva = factureMobileCall.tvaList,
                    };

                    return new JsonResult(await venteProduitsService.CreateGratuite(gratuite));
                }
                else
                {
                    List<VenteDetailsModel> details = new List<VenteDetailsModel>();
                    foreach (var facture in factureMobileCall.factureMobiles)
                    {
                        var coutRevient = produitVendableService.findFormulaireFormeProduit(facture.FormeProduitId).FormeProduit_CoutDeRevient;
                        VenteDetailsModel detail = new VenteDetailsModel()
                        {
                            VenteDetails_Prix = facture.Prix,
                            VenteDetails_Quantite = facture.Quantite,
                            VenteDetails_UniteId = facture.UniteId,
                            VenteDetails_FormeProduitId = facture.FormeProduitId,
                            VenteDetails_CoutDeRevient = coutRevient * facture.Quantite,
                            VenteDetails_Marge = facture.Prix - (coutRevient * facture.Quantite),
                        };

                        details.Add(detail);
                    }
                    VenteModel vente = new VenteModel()
                    {
                        Vente_AbonnementId = aboId,
                        Vente_PointVenteId = pdvId,
                        Vente_PositionVenteId = (int)posId,
                        Vente_UtilisateurId = factureMobileCall.userId,
                        Vente_Date = DateTime.Now,
                        Vente_TauxDeRemise = (decimal)factureMobileCall.tauxRemise,
                        Vente_PrixTotalRemise = (decimal)factureMobileCall.totalRemise,
                        Vente_Prix = details.Sum(v => v.VenteDetails_Prix),
                        Vente_ModePaiement = (int)factureMobileCall.venteModePaimentId,
                        Vente_Commentaire = factureMobileCall.venteCommentaire,
                        Vente_Marge = details.Sum(v => v.VenteDetails_Marge),
                        Vente_NumeroTicket = factureMobileCall.Vente_NumeroTicket,
                        Details = details,
                        Tva = factureMobileCall.tvaList,
                    };

                    return new JsonResult(await venteProduitsService.CreateVente(vente));
                }
                
            }
            
        }

        [HttpGet]
        [Route("getModePaiement")]
        public JsonResult getModePaiement()
        {
            return new JsonResult(pointVenteService.getListModePaiement());
        }

        [HttpGet("getCategorie/{userId}", Name = "GetCategorie")]
        public JsonResult GetCategorie(string userId)
        {
            var Id = authentificationRepository.Geturser(userId).Abonnement_ID;
            var pdvId = authentificationRepository.Geturser(userId).Position_Vente.PositionVente_PointVenteId;
            return new JsonResult(familleProduitService.getListFamillesByPdv((int)Id, pdvId));
        }
        [HttpGet("getunite", Name = "getunite")]
        public JsonResult getunite()
        {
           // var Id = authentificationRepository.Geturser(userId).Abonnement_ID;
            return new JsonResult(produitVendableService.getListUniteMesure());
        }  
        [HttpGet("getImage/{userId}", Name = "getImage")]
        public JsonResult getImage(string userId)
        {
            var all = new List<ImageApiModel>();
            var Id = authentificationRepository.Geturser(userId).Abonnement_ID;
            var pdvId = authentificationRepository.Geturser(userId).Position_Vente.PositionVente_PointVenteId;
            var stocks = pointVenteService.getStockPointVente((int)Id, pdvId);
            var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto });
            foreach(var s in stock)
            {
                var Image = new ImageApiModel()
                {
                    image =  s.Key.photo,
                };
                if (s.Key.photo != null)
                    all.Add(Image);
            }
            var pp = pointVenteService.getStockProduitPackage((int)Id, pdvId).Where(p => p.formes.Count() > 0);
            var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            foreach (var produit in package)
            {
                var Image = new ImageApiModel()
                {
                    image = produit.ProduitPackage_Photo,
                };
                if (produit.ProduitPackage_Photo != null)
                    all.Add(Image);
            }
            var prt = produitVendableService.getListProduitConsomableEnMagasin((int)Id, pdvId, null);
            foreach (var produit in prt)
            {
                var Image = new ImageApiModel()
                {
                    image =  produit.ProduitPretConsomer_Photo,
                };
                if (produit.ProduitPretConsomer_Photo != null)
                    all.Add(Image);
            }
            var familles = familleProduitService.getListFamillesByPdv((int)Id, pdvId);
            foreach (var fam in familles)
            {
                var sousFamilles = familleProduitService.getListSousFamilles(fam.FamilleProduit_Id);
                foreach(var sousFam in sousFamilles)
                {
                    var ImageS = new ImageApiModel()
                    {
                        image = sousFam.SousFamille_Image,
                    };
                    if (sousFam.SousFamille_Image != null)
                        all.Add(ImageS);
                }
                var Image = new ImageApiModel()
                {
                    image = fam.FamilleProduit_Image,
                };
                if (fam.FamilleProduit_Image != null)
                    all.Add(Image);
            }

            return new JsonResult(all);
        }
        [HttpGet("getUsers/{userId}", Name = "getUsers")]
        public async Task<JsonResult> getUsersAsync(string userId)
        {
            var pdvId = authentificationRepository.Geturser(userId).Position_Vente.PositionVente_PointVenteId;
            var users = await pointVenteService.getUsersAsync(pdvId);
            return  new JsonResult(users);


        }
    }
}


