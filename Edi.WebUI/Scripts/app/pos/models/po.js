define([
    'underscore',
    'backbone'
], function(_, Backbone) {
    var Po = Backbone.Model.extend({
        url: function() {
            return '/api/PurchaseOrderApi/' + this.get('ID');
        },

        defaults: {
            "ID": 0,
            "CustomerID": "",
            "BEG01_TransactionSetPurposeCode": "",
            "BEG02_PurchaseOrderTypeCode": "",
            "BEG03_PurchaseOrderNumber": "",
            "BEG05_Date": "",
            "CUR01_CurrencyEntityIdentifierCode": "",
            "CUR02_CurrencyCode": "",
            "CTT01_NumberofLineItems": 0,
            "AMT01_AmountQualifierCode": "",
            "AMT02_Amount": 0,
            "Dtms": [],
            "Items": [],
            "Names": [],
            "PoEnvelope": {},
            "Refs": []
        },

        idAttribute: 'ID',

        validate: function(attrs, options) {
            // Validate top-level Dtm
            var today = new Date();
            today.setHours(0, 0, 0, 0);
            for (var j = 0; j < attrs.Dtms.length; j++) {
                for (var k = 0; k < attrs.Dtms.length; k++) {
                    if (j !== k) {
                        if (attrs.Dtms[j].DTM01_DateTimeQualifier === attrs.Dtms[k].DTM01_DateTimeQualifier) {
                            return "Repeated Dtm";
                        }
                    }
                }

                if (attrs.Dtms[j].DTM02_PurchaseOrderDate === "") {
                    return "Please remember to enter the date";
                }

                var poDate = new Date(attrs.Dtms[j].DTM02_PurchaseOrderDate);
                var offset = poDate.getTimezoneOffset();
                poDate.setMinutes(offset);
                if (poDate < today) {
                    return "Please enter a date that is today or later";
                }
            }

            // Validate Item
            for (var i = 0; i < attrs.Items.length; i++) {
                if (attrs.Items[i].PO107_ProductID === "" ||
                    attrs.Items[i].PO104_UnitPrice === "" ||
                    attrs.Items[i].PO102_QuantityOrdered === "") {
                    return "You are missing line item values";
                }
            }
        }
    });

    return Po;
});