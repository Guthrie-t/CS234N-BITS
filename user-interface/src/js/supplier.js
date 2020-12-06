/*  Brewery Inventory Tracking System — Supplier Page

    Manages the supplier database and interacts with the internally facing UI 
    for the company. It renders partial supplier content until the client 
    expands the details accordion to reveal the rest. 

    Client will be able to add, edit, delete, archive supplier data.
*/

class SupplierPage {
    constructor() {
        // FL: I setup the state object to reflect a single Supplier at a time. 
        //     My thought process was to insert the chosen supplier into the state 
        //     when the Client expands the details view. It may be better to 
        //     reconstruct the state object to hold all supplier data (or have 
        //     a suppliers array included).
        this.state = {
            supplierId: "",
            supplier: null,
            address: []
        };
        // FL: test includes real data from the supplier database as an example for 
        //     setup and testing before we make api calls.
        this.test = {
            supplierId: 6,
            supplier: {
                name: "White Labs",
                phone: "n",
                email: "info@whitelabs.com",
                website: "https://www.whitelabs.com/",
                repFirst: "Kim",
                repLast: "Derr",
                repPhone: "858.267.7691",
                repEmail: "kderr@whitelabs.com ",
                note: ""
            },
            addresses: [
                {
                    addressId: 7,                // FL: When we fetch the data, if we change this value to
                    addressType: "Billing", // 1 //     the english address type, we can use it in the string
                    address: {                   //     literal on the DOM for underneath the Address line
                        streetLine1: ": 9495 Candida Street",
                        streetLine2: "",
                        city: "San Diego",
                        state: "CA",
                        zipcode: "92126",
                        country: "USA"
                    }
                },
                {
                    addressId: 7,
                    addressType: "Mailing", // 2
                    address: {
                        streetLine1: ": 9495 Candida Street",
                        streetLine2: "",
                        city: "San Diego",
                        state: "CA",
                        zipcode: "92126",
                        country: "USA"
                    }
                }
            ]
        };
        // api urls
        this.server = "https://localhost:58543/api/";
        this.url = this.server + "/supplier";
        // ui elements
        this.selectAlphaList = document.querySelector('#selectAlphaList');
        this.selectIngredientList = document.querySelector('#selectIngredientList');
        this.searchForm = document.querySelector('#searchForm');
        this.selectView = document.querySelector('#selectView');
        this.bulkControlSet = document.querySelector('#bulkControlSet');
        this.bulkEdit = document.querySelector('#bulkEdit');
        this.bulkDelete = document.querySelector('#bulkDelete');
        this.bulkArchive = document.querySelector('#bulkArchive');
        this.addSupplier = document.querySelector('#addSupplier');
        this.loadingIndicator = document.querySelector('#loadingIndicator');
        this.mainContent = document.querySelector('#mainContent');
        this.supplierTable = document.querySelector('#supplierTable');
        this.selectAllCheckbox = document.querySelector('#selectAllCheckbox');
        this.selectRecord = document.querySelector('.select-record');
        this.summary = document.querySelector('.summary');
        this.detailsCaret = document.querySelector('.details-caret');
        this.details = document.querySelector('.details');
        // field elements may need to be supplierId specific 
        this.supplierName = document.querySelector(`td.supplierName input`);
        this.repName = document.querySelector(`td.repName input`);
        this.repPhone = document.querySelector(`td.repPhone input`);
        // this.supplierPhone = document.querySelector(`${this.test.supplierId} td.supplierPhone input`);
        // this.supplierEmail = document.querySelector(`${this.test.supplierId} td.supplierEmail input`);
        // this.website = document.querySelector(`${this.test.supplierId} td.website input`);
        // this.streetLine1 = document.querySelector(`details-${this.test.supplierId} td.streetLine1 input`);
        // this.streetLine2 = document.querySelector(`details-${this.test.supplierId} td.streetLine2 input`);
        // this.city = document.querySelector(`details-${this.test.supplierId} td.city input`);
        // this.state = document.querySelector(`details-${this.test.supplierId} td.state input`);
        // this.zipcode = document.querySelector(`details-${this.test.supplierId} td.zipcode input`);
        // this.country = document.querySelector(`details-${this.test.supplierId} td.country input`);
        this.crudControls = document.getElementsByClassName('.crud-controls');
        this.edit = document.getElementsByClassName('crud-controls.edit');
        this.save = document.getElementsByClassName('crud-controls.save');
        this.delete = document.getElementsByClassName('crud-controls.delete');
        this.archive = document.getElementsByClassName('crud-controls.archive');
        // start application
        console.log("loaded");
        this.bindAllMethods.bind(this);
        this.loadData();
        this.addEventListeners();
    }

    // HELPER METHODS
    bindAllMethods() {
        this.addEventListeners = this.addEventListeners.bind(this);
        this.loadData = this.loadData.bind(this);
        this.renderTable = this.renderTable.bind(this);
        this.renderRecordSummary = this.renderRecordSummary.bind(this);
        this.renderRecordDetails = this.renderRecordDetails.bind(this);
        this.fetchAllSuppliers = this.fetchAllSuppliers.bind(this);
        this.fetchSupplier = this.fetchSupplier.bind(this);
        this.enableFields = this.enableFields.bind(this);
        this.selectAllCheckboxes = this.selectAllCheckboxes.bind(this);
        this.addSupplier = this.addSupplier.bind(this);
        this.updateSupplier = this.updateSupplier.bind(this);
        this.deleteSupplier = this.deleteSupplier.bind(this);
        this.archiveSupplier = this.archiveSupplier.bind(this);
    }
    addEventListeners() {
        console.log('addEventListeners');
        // FL: I was attempting to setup onclick eventListeners on the 
        //     crud-controls links at the bottom right of the details view.
        //     It needs to preventDefault() browser action and call the 
        //     correct method. Perhaps it would work if it wasn't an 
        //     anchor tag with an href attribute?
        this.edit.onclick = this.enableFields;
        this.save.onclick = this.updateSupplier;
        this.delete.onclick = this.deleteSupplier;
        this.archive.onclick = this.archiveSupplier;
    }

    // LOAD DATA
    loadData() {
        console.log("loadData");
        // fetch each record in the supplier db
        this.fetchAllSuppliers();
        // hide the loading icon before renderTable
        this.loadingIndicator.classList.add('d-none');
        // then renderTable();
        this.renderTable(); 
    }

    // GET SUPPLIER DATA
    fetchAllSuppliers() {
        console.log("fetchAllSuppliers");
        // get data for every supplier in the database 
        this.fetchSupplier();
    }
    fetchSupplier() { 
        console.log("fetchSupplier");
        // fetch single supplier
        // swap address type int to string for DOM output
        // example: if addressType == 1, then addressType = "Billing"
    }  

    // RENDER SUPPLIER DATA
    renderTable() { 
        console.log("renderTable");
        this.mainContent.innerHTML = `
            <div id="supplierTable" class="table-responsive">
                <table class="table mb-0">
                    <thead class="text-muted">
                        <tr>
                            <th scope="col">
                                <div class="form-check">
                                    <input id="selectAllCheckbox" class="form-check-input position-static" type="checkbox" value="" aria-label="Select all vendor records">
                                </div>
                            </th>
                            <th scope="col">#</th>
                            <th scope="col">Vendor</th>
                            <th scope="col">Primary Contact</th>
                            <th scope="col">Phone</th>
                            <th scope="col">Office</th>
                            <th scope="col">Email</th>
                            <th scope="col">Website</th>
                            <th scope="col">&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${this.renderRecordSummary()}
                    </tbody>
                </table>
            </div>
        `;
    }
    renderRecordSummary() { 
        console.log("renderSupplierRecord");
        return `
            <tr class="summary" data-id="${this.test.supplierId}">
                <th scope="row">
                    <div class="form-check">
                        <input class="form-check-input position-static select-record" type="checkbox" value="option1" aria-label="Select vendor">
                    </div>
                </th>
                <td>${this.test.supplierId}</td>
                <td class="supplierName">
                    <input type="text" disabled class="form-control-plaintext" value="${this.test.supplier.name}">
                </td>
                <td class="repName">
                    <input type="text" disabled class="form-control-plaintext" value="${this.test.supplier.repFirst} ${this.test.supplier.repLast}">
                </td>
                <td class="repPhone">
                    <a href="tel:${this.test.supplier.repPhone}">
                        <input type="text" disabled class="form-control-plaintext" value="${this.test.supplier.repPhone}">
                    </a>
                </td>
                <td class="supplierPhone">
                    <a href="tel:${this.test.supplier.phone}">
                        <input type="text" disabled class="form-control-plaintext" value="${this.test.supplier.phone}">
                    </a>
                </td>
                <td class="supplierEmail">
                    <a href="mailto:${this.test.supplier.email}">
                        <input type="text" disabled class="form-control-plaintext" value="${this.test.supplier.email}">
                    </a>
                </td>
                <td class="website">
                    <a href="${this.test.supplier.website}" target="_blank">
                        <input type="text" disabled class="form-control-plaintext" value="${this.test.supplier.website}">
                    </a>
                </td>
                <td>
                    <a class="details-caret" href="#details-${this.test.supplierId}" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="Supplier details">
                        <i class="fa fa-caret-down"></i>
                    </a>
                </td>
            </tr>
            ${this.renderRecordDetails()}
        `;
    }

    renderRecordDetails() {
        console.log("renderRecordDetails");
        return `
            <tr class="table-nested details collapse" id="details-${this.test.supplierId}">
                <th>&nbsp;</th>
                <td colspan="8" class="">
                    <div class="row mb-4">
                        <div class="col-12">
                            <table class="table table-sm mb-0">
                                <tbody>
                                    <tr class="address">
                                        <th scope="row" class="streetLine1">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.test.addresses[0].address.streetLine1}">
                                        </th>
                                        <td class="streetLine2">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.test.addresses[0].address.streetLine2}">
                                        </td>
                                        <td class="city">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.test.addresses[0].address.city}">
                                        </td>
                                        <td class="state">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.test.addresses[0].address.state}">
                                        </td>
                                        <td class="zipcode">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.test.addresses[0].address.zipcode}">
                                        </td>
                                        <td class="country">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.test.addresses[0].address.country}">
                                        </td>
                                    </tr>
                                    <tr class="address-label text-muted small">
                                        <th scope="row">${this.test.addresses[0].addressType} Address Line 1</th>
                                        <th scope="row">${this.test.addresses[0].addressType} Address Line 2</th>
                                        <td>City</td>
                                        <td>State / Province</td>
                                        <td>Zip-code</td>
                                        <td>Country</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="row">    
                        <div class="col-12">
                            <div class="form-group">
                                <label>Notes:</label>
                                <textarea class="form-control notes" disabled>${this.test.supplier.note}</textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">    
                        <div class="col text-muted">
                            <nav class="nav">
                                <a class="nav-link h5" href="#">Order history</a>
                                <a class="nav-link h5" href="#">Ingredients</a>
                            </nav>
                        </div>
                        <div class="col">
                            <div class="crud-controls text-right">
                                <button class="edit btn btn-outline-dark">Edit</button>
                                <button class="save btn btn-outline-dark">Save</button>
                                <button class="archive btn btn-outline-dark">Archive</button>
                                <button class="delete btn btn-outline-dark">Delete</button>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        `;
    }

    // UI INTERACTIONS
    enableFields(enabled=false) { 
        console.log("enableFields"); 
        // removes disabled from input fields
        // FL: Not sure why I can't get this to work yet either... I feel like basically 
        //     nothing I am doing is working and I just need to go to sleep now.
        this.supplierName.disabled = enabled;
        this.repName.disabled = enabled;
        this.repPhone.disabled = enabled;
        this.supplierPhone.disabled = enabled;
        this.supplierEmail.disabled = enabled;
        this.website.disabled = enabled;
        this.streetLine1.disabled = enabled;
        this.streetLine2.disabled = enabled;
        this.city.disabled = enabled;
        this.state.disabled = enabled;
        this.zipcode.disabled = enabled;
        this.country.disabled = enabled;
    } 
    selectAllCheckboxes() { 
        console.log("selectAllCheckboxes"); 
        // when this.selectAllCheckbox is checked, select all checkboxes in the table
    }
    
    // CRUD METHODS
    addSupplier() { 
        console.log("addSupplier"); 
    }
    updateSupplier() { 
        console.log("updateSupplier");
    }
    deleteSupplier() { 
        console.log("deleteSupplier");
    }
    archiveSupplier() { 
        console.log("archiveSupplier"); 
    }

    // OPTIONAL
    // editBulkSuppliers() { console.log("editBulkSuppliers") }
    // deleteBulkSuppliers() { console.log("deleteBulkSuppliers") }
    // archiveBulkSuppliers() { console.log("archiveBulkSuppliers") }
}

// create the instance on page load
window.onload = () => new SupplierPage();