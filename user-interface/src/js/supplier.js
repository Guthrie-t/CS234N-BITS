/*  Brewery Inventory Tracking System — Supplier Page

    Manages the supplier database and interacts with the internally facing UI 
    for the company. It renders partial supplier content until the client 
    expands the details accordion to reveal the rest. 

    Client will be able to add, edit, delete, archive supplier data.
*/

class SupplierPage {
    constructor() {
        this.state = {
            suppliers: [], // array of all supplier objects
            addresses: [], // array of all supplier address objects
            selectSupplier: { // grab the chosen supplier by Id and all associated data; is this useful or an unnecessary step?
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
            }
        }
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
        this.addSupplierBtn = document.querySelector('#addSupplier');
        this.loadingIndicator = document.querySelector('#loadingIndicator');
        this.mainContent = document.querySelector('#mainContent');
        //start application
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
        // foreach (c in this.detailsCaret)
        //     this.detailsCaret.onclick = this.renderRecordDetails;
        this.enableFields();

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
        // for each supplier, generate html
        return `
            <tr class="summary" id="summary-${this.state.selectSupplier.supplierId}">
                <th scope="row">
                    <div class="form-check">
                        <input class="form-check-input position-static select-record" type="checkbox" value="option1" aria-label="Select vendor">
                    </div>
                </th>
                <td>${this.state.selectSupplier.supplierId}</td>
                <td>
                    <input id="supplierName" name="supplierName" type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.supplier.name}">
                </td>
                <td class="repName">
                    <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.supplier.repFirst} ${this.state.selectSupplier.supplier.repLast}">
                </td>
                <td class="repPhone">
                    <a href="tel:${this.state.selectSupplier.supplier.repPhone}">
                        <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.supplier.repPhone}">
                    </a>
                </td>
                <td class="supplierPhone">
                    <a href="tel:${this.state.selectSupplier.supplier.phone}">
                        <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.supplier.phone}">
                    </a>
                </td>
                <td class="supplierEmail">
                    <a href="mailto:${this.state.selectSupplier.supplier.email}">
                        <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.supplier.email}">
                    </a>
                </td>
                <td class="website">
                    <a href="${this.state.selectSupplier.supplier.website}" target="_blank">
                        <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.supplier.website}">
                    </a>
                </td>
                <td>
                    <a class="details-caret" href="#details-${this.state.selectSupplier.supplierId}" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="Supplier details">
                        <i class="fa fa-caret-down"></i>
                    </a>
                </td>
            </tr>
            ${this.renderRecordDetails()}
        `;
    }

    renderRecordDetails() {
        // triggered by onclick event of detailsCaret[i]
        console.log("renderRecordDetails");
        return `
            <tr class="table-nested details collapse" id="details-${this.state.selectSupplier.supplierId}">
                <th>&nbsp;</th>
                <td colspan="8" class="">
                    <div class="row mb-4">
                        <div class="col-12">
                            <table class="table table-sm mb-0">
                                <tbody>
                                    <tr class="address">
                                        <th scope="row" class="streetLine1">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.addresses[0].address.streetLine1}">
                                        </th>
                                        <td class="streetLine2">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.addresses[0].address.streetLine2}">
                                        </td>
                                        <td class="city">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.addresses[0].address.city}">
                                        </td>
                                        <td class="state">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.addresses[0].address.state}">
                                        </td>
                                        <td class="zipcode">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.addresses[0].address.zipcode}">
                                        </td>
                                        <td class="country">
                                            <input type="text" disabled class="form-control-plaintext" value="${this.state.selectSupplier.addresses[0].address.country}">
                                        </td>
                                    </tr>
                                    <tr class="address-label text-muted small">
                                        <th scope="row">${this.state.selectSupplier.addresses[0].addressType} Address Line 1</th>
                                        <th scope="row">${this.state.selectSupplier.addresses[0].addressType} Address Line 2</th>
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
                                <textarea class="form-control notes" disabled>${this.state.selectSupplier.supplier.note}</textarea>
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
                                <button id="editBtn" class="edit btn btn-outline-dark">Edit</button>
                                <button id="saveBtn" class="save btn btn-outline-dark">Save</button>
                                <button id="archiveBtn" class="archive btn btn-outline-dark">Archive</button>
                                <butto id="deleteBtn" class="delete btn btn-outline-dark">Delete</button>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        `;
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

    // UI INTERACTIONS
    enableFields(enabled=false) { 
        // triggered by an onclick event of editBtn
        // removes disabled from input fields
        console.log("enableFields"); 
    } 
    selectAllCheckboxes() { 
        // triggered by checking the selectAllCheckbox
        console.log("selectAllCheckboxes"); 
    }

    
    // CRUD METHODS
    addSupplier(e) { 
        // triggered by an onclick event of addSupplierBtn
        console.log("addSupplier"); 
        e.preventDefault();
    }
    updateSupplier() { 
        // triggered by an onclick event of saveBtn
        console.log("updateSupplier");
    }
    deleteSupplier() { 
        // triggered by an onclick event of deleteBtn
        console.log("deleteSupplier");
    }
    archiveSupplier() { 
        // triggered by an onclick event of archiveBtn
        console.log("archiveSupplier"); 
    }

    // OPTIONAL
    // editBulkSuppliers() { console.log("editBulkSuppliers") }
    // deleteBulkSuppliers() { console.log("deleteBulkSuppliers") }
    // archiveBulkSuppliers() { console.log("archiveBulkSuppliers") }
}


// create the instance on page load
window.onload = () => new SupplierPage;