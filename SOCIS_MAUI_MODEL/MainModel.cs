using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SOCIS_MAUI_MODEL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Azure;
using System.Collections.ObjectModel;

namespace SOCIS_MAUI_MODEL
{
    public sealed class MainModel
    {
        private Person _userInSystem;
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonSerializerOptions;
        private IConfiguration _config;
        private ObservableCollection<Place> _places;
        private ObservableCollection<Firm> _Firms;
        private ObservableCollection<UnitType> _UnitTypes;
        private ObservableCollection<FullNameUnit> _FullNameUnits;
        private ObservableCollection<AccountingUnit> _AccountingUnits;
        private ObservableCollection<Person> _Persons;
        public MainModel(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_config["BaseAddress"]);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["Token"]);
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        #region Person
        public ObservableCollection<Person> GetAllPersons()
        {
            if (_Persons is null)
            {
                _Persons = new ObservableCollection<Person>(GetAll<Person>("Person/GetAll").OrderBy(x => x.Surname));
            }
            return _Persons;
        }
        #endregion
        #region Place
        public ObservableCollection<Place> GetAllPlaces()
        {
            if (_places is null)
            {
                _places = new ObservableCollection<Place>(GetAll<Place>("Place/GetAll").OrderBy(x=>x.Name));
            }
            return _places;
        }
        public void PlaceAdd(Place place)
        {
            var newPlace=Add(place, "Place/Add");
            _places.Add(newPlace);
        }
        public void PlaceUpdate(int placeId,Place newPlace)
        {
            Update(newPlace, placeId, "Place/Update");
            var oldPlace = _places.First(x => x.Id == placeId);
            oldPlace = newPlace;
        }
        public void PlaceDelete(Place place)
        {
            Delete($"Place/Delete/{place.Id}");
            _places.Remove(place);
        }
        #endregion

        #region Firm
        public ObservableCollection<Firm> GetAllFirms()
        {
            if (_Firms is null)
            {
                _Firms = new ObservableCollection<Firm>(GetAll<Firm>("Firm/GetAll"));
            }
            return _Firms;
        }
        public void FirmAdd(Firm Firm)
        {
            var newFirm = Add(Firm, "Firm/Add");
            _Firms.Add(newFirm);
        }
        public void FirmUpdate(int FirmId, Firm newFirm)
        {
            Update(newFirm, FirmId, "Firm/Update");
            var oldFirm = _Firms.First(x => x.Id == FirmId);
            oldFirm = newFirm;
        }
        public void FirmDelete(Firm Firm)
        {
            Delete($"Firm/Delete/{Firm.Id}");
            _Firms.Remove(Firm);
        }
        #endregion

        #region FullNameUnit
        public ObservableCollection<FullNameUnit> GetAllFullNameUnits()
        {
            if (_FullNameUnits is null)
            {
                _FullNameUnits = new ObservableCollection<FullNameUnit>(GetAll<FullNameUnit>("FullNameUnit/GetAll"));
            }
            return _FullNameUnits;
        }
        public void FullNameUnitAdd(FullNameUnit FullNameUnit)
        {
            var FullNameUnitDTO = new FullNameUnit
            {
                FirmId = FullNameUnit.FirmId,
                UnitTypeId = FullNameUnit.UnitTypeId,
                Model = FullNameUnit.Model,
                ModelNo = FullNameUnit.ModelNo
            };
            var newFullNameUnit = Add(FullNameUnitDTO, "FullNameUnit/Add");
            FullNameUnit.Id = newFullNameUnit.Id;
            _FullNameUnits.Add(FullNameUnit);
        }
        public void FullNameUnitUpdate(int FullNameUnitId, FullNameUnit newFullNameUnit)
        {
            var FullNameUnitDTO = new FullNameUnit
            {
                Id=newFullNameUnit.Id,
                FirmId = newFullNameUnit.FirmId,
                UnitTypeId = newFullNameUnit.UnitTypeId,
                Model = newFullNameUnit.Model,
                ModelNo = newFullNameUnit.ModelNo
            };
            Update(FullNameUnitDTO, FullNameUnitId, "FullNameUnit/Update");
            var oldFullNameUnit = _FullNameUnits.First(x => x.Id == FullNameUnitId);
            oldFullNameUnit = newFullNameUnit;
        }
        public void FullNameUnitDelete(FullNameUnit FullNameUnit)
        {
            Delete($"FullNameUnit/Delete/{FullNameUnit.Id}");
            _FullNameUnits.Remove(FullNameUnit);
        }
        #endregion
        #region UnitPlace
        public ObservableCollection<UnitPlace> GetAllUnitPlaceByUnit(int id)
        {
            return new ObservableCollection<UnitPlace>(GetAll<UnitPlace>($"UnitPlace/GetAllByUnit/{id}"));
        }
        public ObservableCollection<UnitPlace> GetAllUnitPlaceByPlace(int id)
        {
            var unPlaces = new ObservableCollection<UnitPlace>(GetAll<UnitPlace>($"UnitPlace/GetAllOldByPlace/{id}"));
            foreach(var item in GetAll<UnitPlace>($"UnitPlace/GetAllActiveByPlace/{id}"))
            {
                unPlaces.Add(item);
            }
            return unPlaces;
        }
        public void UnitPlaceDelete(UnitPlace UnitPlace)
        {
            Delete($"UnitPlace/Delete/{UnitPlace.Id}");
        }
        public void UnitPlaceAdd(UnitPlace UnitPlace)
        {
            Add(UnitPlace, "UnitPlace/Add");
        }
        public void UnitPlaceUpdate(int UnitPlaceId, UnitPlace newUnitPlace)
        {
            var UnitPlaceDTO = new UnitPlace
            {
                Id = newUnitPlace.Id,
                UnitId = newUnitPlace.UnitId,
                PlaceId = newUnitPlace.PlaceId,
                Comment = newUnitPlace.Comment,
                DateStart = newUnitPlace.DateStart,
                DateEnd = newUnitPlace.DateEnd
            };
            Update(UnitPlaceDTO, UnitPlaceId, "UnitPlace/Update");
        }
        #endregion

        #region UnitRespPerson
        public ObservableCollection<UnitRespPerson> GetAllUnitRespPersonByUnit(int id)
        {
            return new ObservableCollection<UnitRespPerson>(GetAll<UnitRespPerson>($"UnitRespPerson/GetAllByUnit/{id}"));
        }
        public ObservableCollection<UnitRespPerson> GetAllUnitRespPersonByPerson(int id)
        {
            var unitRespPeople = new ObservableCollection<UnitRespPerson>(GetAll<UnitRespPerson>($"UnitRespPerson/GetAllActiveByPerson/{id}"));
            foreach(var item in GetAll<UnitRespPerson>($"UnitRespPerson/GetAllOldByPerson/{id}"))
            {
                unitRespPeople.Add(item);
            }
            return unitRespPeople;
        }
        public void UnitRespPersonDelete(UnitRespPerson UnitRespPerson)
        {
            Delete($"UnitRespPerson/Delete/{UnitRespPerson.Id}");
        }
        public void UnitRespPersonAdd(UnitRespPerson UnitRespPerson)
        {
            Add(UnitRespPerson, "UnitRespPerson/Add");
        }
        public void UnitRespPersonUpdate(int UnitRespPersonId, UnitRespPerson newUnitRespPerson)
        {
            Update(newUnitRespPerson, UnitRespPersonId, "UnitRespPerson/Update");
        }
        #endregion
        #region ShortTermMove
        public ObservableCollection<ShortTermMove> GetAllShortTermMoveByUnit(int id)
        {
            return new ObservableCollection<ShortTermMove>(GetAll<ShortTermMove>($"ShortTermMove/GetAllByUnit/{id}"));
        }
        public ObservableCollection<ShortTermMove> GetAllShortTermMovesActive()
        {
            return new ObservableCollection<ShortTermMove>(GetAll<ShortTermMove>($"ShortTermMove/GetAllActive"));
        }
        public ObservableCollection<ShortTermMove> GetAllShortTermMoveByPlace(int id)
        {
            var unPlaces = new ObservableCollection<ShortTermMove>(GetAll<ShortTermMove>($"ShortTermMove/GetAllOldByPlace/{id}"));
            foreach (var item in GetAll<ShortTermMove>($"ShortTermMove/GetAllActiveByPlace/{id}"))
            {
                unPlaces.Add(item);
            }
            return unPlaces;
        }
        public void ShortTermMoveDelete(ShortTermMove ShortTermMove)
        {
            Delete($"ShortTermMove/Delete/{ShortTermMove.ShortTermMoveId}");
        }
        public void ShortTermMoveAdd(ShortTermMove ShortTermMove)
        {
            Add(ShortTermMove, "ShortTermMove/Add");
        }
        public void ShortTermMoveUpdate(int ShortTermMoveId, ShortTermMove newShortTermMove)
        {
            var ShortTermMoveDTO = new ShortTermMove
            {
                ShortTermMoveId = newShortTermMove.ShortTermMoveId,
                UnitId = newShortTermMove.UnitId,
                PlaceId = newShortTermMove.PlaceId,
                DateTimeStart = newShortTermMove.DateTimeStart,
                DateTimeEndFact = newShortTermMove.DateTimeEndFact,
                DateTimeEndPlan = newShortTermMove.DateTimeEndPlan
            };
            Update(ShortTermMoveDTO, ShortTermMoveId, "ShortTermMove/Update");
        }
        #endregion
        #region AccountingUnit
        public ObservableCollection<AccountingUnit> GetAllAccountingUnits()
        {
            if (_AccountingUnits is null)
            {
                _AccountingUnits = new ObservableCollection<AccountingUnit>(GetAll<AccountingUnit>("AccountingUnit/GetAll"));
            }
            return _AccountingUnits;
        }
        public void AccountingUnitAdd(AccountingUnit AccountingUnit)
        {
            var AccountingUnitDTO = new AccountingUnit
            {
                Mac = AccountingUnit.Mac,
                SerNum = AccountingUnit.SerNum,
                NetName = AccountingUnit.NetName,
                ManufDate = AccountingUnit.ManufDate,
                InvNum = AccountingUnit.InvNum,
                FullNameUnitId = AccountingUnit.FullNameUnitId,
                Comment = AccountingUnit.Comment
            };
            var newAccountingUnit = Add(AccountingUnitDTO, "AccountingUnit/Add");
            AccountingUnit.Id = newAccountingUnit.Id;
            if (_AccountingUnits is not null)
            _AccountingUnits.Add(AccountingUnit);
        }
        public void AccountingUnitUpdate(int AccountingUnitId, AccountingUnit newAccountingUnit)
        {
            var AccountingUnitDTO = new AccountingUnit
            {
                Id=newAccountingUnit.Id,
                Mac = newAccountingUnit.Mac,
                SerNum = newAccountingUnit.SerNum,
                NetName = newAccountingUnit.NetName,
                ManufDate = newAccountingUnit.ManufDate,
                InvNum = newAccountingUnit.InvNum,
                FullNameUnitId = newAccountingUnit.FullNameUnitId,
                Comment = newAccountingUnit.Comment
            };
            Update(AccountingUnitDTO, AccountingUnitId, "AccountingUnit/Update");
            if (_AccountingUnits is not null)
            {
                var oldAccountingUnit = _AccountingUnits.First(x => x.Id == AccountingUnitId);
                oldAccountingUnit = newAccountingUnit;
            }
        }
        public void AccountingUnitDelete(AccountingUnit AccountingUnit)
        {
            Delete($"AccountingUnit/Delete/{AccountingUnit.Id}");
            _AccountingUnits.Remove(AccountingUnit);
        }
        #endregion
        #region UnitType
        public ObservableCollection<UnitType> GetAllUnitTypes()
        {
            if (_UnitTypes is null)
            {
                _UnitTypes = new ObservableCollection<UnitType>(GetAll<UnitType>("UnitType/GetAll"));
            }
            return _UnitTypes;
        }
        public void UnitTypeAdd(UnitType UnitType)
        {
            var newUnitType = Add(UnitType, "UnitType/Add");
            _UnitTypes.Add(newUnitType);
        }
        public void UnitTypeUpdate(int UnitTypeId, UnitType newUnitType)
        {
            Update(newUnitType, UnitTypeId, "UnitType/Update");
            var oldUnitType = _UnitTypes.First(x => x.Id == UnitTypeId);
            oldUnitType = newUnitType;
        }
        public void UnitTypeDelete(UnitType UnitType)
        {
            Delete($"UnitType/Delete/{UnitType.Id}");
            _UnitTypes.Remove(UnitType);
        }
        #endregion

        #region Auth
        public bool IsAuth()
        {
            var response = _httpClient.GetAsync("Auth/IsAuth").Result;
            return response.IsSuccessStatusCode;
        }
        public bool Authorize(string username,string password)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, $"Auth/Authorize?username={username}&password={password}");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadFromJsonAsync<Token>(_jsonSerializerOptions).Result;
                _config["Token"] = token.token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token.token);
                return true;
            }
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        public bool IsAdminMode()
        {
            if (_userInSystem is null) SetUser();
            return _userInSystem.Role.Name == "admin" ? true : false;
        }
        private void SetUser()
        {
            var response = _httpClient.GetAsync("crud/Person/GetMy").Result;
            _userInSystem = response.Content.ReadFromJsonAsync<Person>(_jsonSerializerOptions).Result;
        }
        #endregion

        #region Requests
        public List<T> GetAll<T>(string uri)
        {
            var response = _httpClient.GetAsync("crud/"+uri).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<List<T>>(_jsonSerializerOptions).Result;
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        public T Get<T>(string uri,int id)
        {
            var response = _httpClient.GetAsync("crud/" + uri+"/"+id).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions).Result;
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        public T Add<T>(T item,string uri)
        {
            string JsonObj = JsonSerialize(item);
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "crud/"+uri);
            httpRequest.Content = new StringContent(JsonObj, Encoding.UTF8, "application/json");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions).Result;
            }
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        public bool Delete(string uri)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, "crud/" + uri);
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        public bool Update<T>(T item,int itemId,string uri)
        {
            string JsonObj = JsonSerialize(item);
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, $"crud/{uri}/{itemId}");
            httpRequest.Content = new StringContent(JsonObj, Encoding.UTF8, "application/json");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        #endregion

        #region Json
        public string JsonSerialize<T>(T obj)
        {
            return JsonSerializer.Serialize<T>(obj, _jsonSerializerOptions);
        }
        #endregion
    }
}
