using Mozu.Api;
using Mozu.Api.Resources.Commerce.Customer;
using Mozu.Api.Resources.Commerce.Customer.Accounts;
using Mozu.Api.Resources.Commerce.Customer.Credits;
using Mozu.Api.Contracts.Customer.Credit;
using Mozu.Api.Resources.Commerce.Customer.Attributedefinition;
using Mozu.Api.Contracts.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mozu.Api.Resources.Commerce;
using Mozu.Api.Contracts.CommerceRuntime.Wishlists;

namespace MozuDataConnector.Domain.Handlers
{
    public class CustomerHandler
    { 
        private Mozu.Api.IApiContext _apiContext;

        public CustomerHandler()
        {
        }

        public CustomerHandler(int tenantId, int? siteId, int? masterCatalogId)
        {
            _apiContext = new Mozu.Api.ApiContext(tenantId, siteId);
        }

        public async Task<CustomerAccount> GetCustomerAccount(int accountId)
        {
            var customerAccountResource = new CustomerAccountResource(_apiContext);
            return await customerAccountResource.GetAccountAsync(accountId);
        }

        public async Task<IEnumerable<CustomerAccount>> GetCustomerAccounts(int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            var customerAccountResource = new CustomerAccountResource(_apiContext);
            var accounts = await customerAccountResource.GetAccountsAsync(startIndex, pageSize, sortBy, filter, null);

            return accounts.Items;
        }

        public async Task<CustomerAuthTicket> AddCustomerAccount(CustomerAccountAndAuthInfo account, Credit credit, Wishlist wishlist)
        {
            var notes = new List<string>();

            var customerAccountResource = new CustomerAccountResource(_apiContext);
            var newAccount = await customerAccountResource.AddAccountAndLoginAsync(account);
            
            notes.Add(string.Format("updatedby:{0},updatedDate:{1},action:{2}", newAccount.CustomerAccount.
                AuditInfo.UpdateBy,newAccount.CustomerAccount.AuditInfo.UpdateDate, "AddAccountAndLoginAsync"));
            
            var customerContactResource = new CustomerContactResource(_apiContext);

            foreach (var contact in account.Account.Contacts)
            {
                contact.AccountId = newAccount.CustomerAccount.Id;
                var newContact = await customerContactResource.AddAccountContactAsync(contact, 
                    newAccount.CustomerAccount.Id);

                notes.Add(string.Format("updatedby:{0},updatedDate:{1},action:{2}", newAccount.CustomerAccount.
                    AuditInfo.UpdateBy, newAccount.CustomerAccount.AuditInfo.UpdateDate, "AddAccountContactAsync"));
            }

            var customerCreditResource = new CreditResource(_apiContext);
            credit.CustomerId = newAccount.CustomerAccount.Id;
            var newCredit = await customerCreditResource.AddCreditAsync(credit);

            notes.Add(string.Format("updatedby:{0},updatedDate:{1},action:{2}", newAccount.CustomerAccount.
                AuditInfo.UpdateBy, newAccount.CustomerAccount.AuditInfo.UpdateDate, "AddCreditAsync"));

            
            var wishListItemResource = new WishlistResource(_apiContext);
            wishlist.CustomerAccountId = newAccount.CustomerAccount.Id;
            var newWishList = await wishListItemResource.CreateWishlistAsync(wishlist);

            notes.Add(string.Format("updatedby:{0},updatedDate:{1},action:{2}", newAccount.CustomerAccount.
                AuditInfo.UpdateBy, newAccount.CustomerAccount.AuditInfo.UpdateDate, "CreateWishlistAsync"));


            var customerSegmentResource = 
                new Mozu.Api.Resources.Commerce.Customer.CustomerSegmentResource(_apiContext);

//            var segmentAccount = await customerSegmentResource.AddSegmentAccountsAsync()

            var customerNoteResource = new CustomerNoteResource(_apiContext);
            foreach (var note in notes)
            {
                var newNote = await customerNoteResource.AddAccountNoteAsync(
                    new CustomerNote()
                    {
                        Content = note
                    },
                    newAccount.CustomerAccount.Id);
            }

            return newAccount;
        }

        public async Task<CustomerAccount> GetCustomerAccount(int tenantId, int? siteId,
            int? masterCatalogId, int accountId)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerAccountResource = new CustomerAccountResource(_apiContext);
            var account = await customerAccountResource.GetAccountAsync(accountId);

            return account;
        }

        public async Task<IEnumerable<CustomerAccount>> GetCustomerAccounts(int tenantId, int? siteId,
            int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerAccountResource = new CustomerAccountResource(_apiContext);
            var accounts = await customerAccountResource.GetAccountsAsync(startIndex, pageSize, sortBy, filter, null);

            return accounts.Items;
        }

        public async Task<CustomerAuthTicket> AddCustomerAccount(int tenantId, int? siteId,
            int? masterCatalogId, CustomerAccountAndAuthInfo account)
        {
            _apiContext = new ApiContext(tenantId, siteId);

            var customerAccountResource = new CustomerAccountResource(_apiContext);
            var newAccount = await customerAccountResource.AddAccountAndLoginAsync(account);
          
            return newAccount;
        }

        public async Task<CustomerAccount> UpdateCustomerAccount(int tenantId, int? siteId,
            int? masterCatalogId, CustomerAccount account)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerAccountResource = new CustomerAccountResource(_apiContext);
            var updatedAccount = await customerAccountResource.UpdateAccountAsync(account, 
                account.Id);

            return updatedAccount;
        }

        public async Task<CustomerContact> GetCustomerContact(int tenantId, int? siteId,
            int? masterCatalogId, int accountId, int contactId)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerContactResource = new CustomerContactResource(_apiContext);
            var contact = await customerContactResource.GetAccountContactAsync(accountId, contactId);

            return contact;
        }

        public async Task<IEnumerable<CustomerContact>> GetCustomerContacts(int accountId, int tenantId, 
            int? siteId, int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerContactResource = new CustomerContactResource(_apiContext);
            var contacts = await customerContactResource.GetAccountContactsAsync(accountId, startIndex, 
                pageSize, sortBy, filter, null);

            return contacts.Items;
        }

        public async Task<CustomerContact> AddCustomerContact(int accountId, CustomerContact contact, 
            int tenantId, int? siteId, int? masterCatalogId)
        {
            _apiContext = new ApiContext(tenantId, siteId);

            var customerContactResource = new CustomerContactResource(_apiContext);
            var newContact = await customerContactResource.AddAccountContactAsync(contact, accountId);

            return newContact;
        }

        public async Task<CustomerContact> UpdateCustomerContact(int tenantId, int? siteId,
            int? masterCatalogId, CustomerContact contact)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerContactResource = new CustomerContactResource(_apiContext);
            var updatedcontact = await customerContactResource.UpdateAccountContactAsync(contact,
                contact.AccountId, contact.Id);

            return updatedcontact;
        }

        public async Task<Credit> GetCustomerCredit(int tenantId, int? siteId,
    int? masterCatalogId, string code)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerCreditResource = new CreditResource(_apiContext);
            var credit = await customerCreditResource.GetCreditAsync(code);

            return credit;
        }

        public async Task<IEnumerable<Credit>> GetCustomerCredits(int accountId, int tenantId,
            int? siteId, int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerCreditResource = new CreditResource(_apiContext);
            var credits = await customerCreditResource.GetCreditsAsync(startIndex, pageSize, 
                sortBy, filter, null);

            return credits.Items;
        }

        public async Task<Credit> AddCustomerCredit(Credit credit, int tenantId, 
            int? siteId, int? masterCatalogId)
        {
            _apiContext = new ApiContext(tenantId, siteId);

            var customerCreditResource = new CreditResource(_apiContext);
            var newCustomerCredit = await customerCreditResource.AddCreditAsync(credit);

            return newCustomerCredit;
        }

        public async Task<Credit> UpdateCustomerCredit(int tenantId, int? siteId,
            int? masterCatalogId, Credit credit)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerCreditResource = new CreditResource(_apiContext);
            var updatedCustomerCredit = await customerCreditResource.UpdateCreditAsync(credit, credit.Code);

            return updatedCustomerCredit;
        }

        public async Task<IEnumerable<CustomerSegment>> GetAccountSegments(int accountId, int tenantId, int? siteId,
            int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var customerSegmentResource = new Mozu.Api.Resources.Commerce.Customer.
                Accounts.CustomerSegmentResource(_apiContext);
            
            var segments = await customerSegmentResource.GetAccountSegmentsAsync(accountId, startIndex, pageSize, sortBy, filter, null);

            return segments.Items;
        }
    }
}
