﻿define([
    'jquery',
    'underscore',
    'backbone',
    'models/po',
    'routers/router',
    'components/dataService',
    'text!PurchaseOrder/PoCreate',
    'text!PurchaseOrder/PoDtm',
    'text!PurchaseOrder/PoLineItem',
    'text!PurchaseOrder/PoItemDtm'
], function($, _, Backbone, Po, Router, dataService, CreateTmpl, DtmTmpl, LineItemTmpl,
            ItemDtmTmpl) {
    var poCreateView = Backbone.View.extend({
        template: _.template(CreateTmpl),
        tagName: 'div',

        events: {
            'click #btnSave': 'createPo',
            'click #btnBack': 'back',
            'click #btnAddDtm': 'addDtm',
            'click #btnRemoveDtm': 'removeDtm',
            'click #btnAddLineItem': 'addLineItem',
            'click #btnRemoveLineItem': 'removeLineItem',
            'click #btnAddItemDtm': 'addItemDtm',
            'click #btnRemoveItemDtm': 'removeItemDtm',
            'click .close': 'closeError',
            'focus .form-control': 'resetError'
        },

        $cache: {
            dtmForm: null,
            lineItemForm: null,
            itemDtmForm: null,
            dtmCount: 0,
            lineItemCount: 0,
            itemDtmCount: 0
        },

        initialize: function() {
            this.model = new Po();
            this.$cache.dtmForm = DtmTmpl;
            this.$cache.lineItemForm = LineItemTmpl;
            this.$cache.itemDtmForm = ItemDtmTmpl;
            this.$cache.dtmCount = 0;
            this.$cache.lineItemCount = 0;
            this.$cache.itemDtmCount = 0;
        },

        render: function() {
            this.$el.html(this.template(this.model.toJSON()));

            this.addDtm();
            this.addLineItem();

            return this;
        },

        addDtm: function() {
            var data = {
                count: this.$cache.dtmCount
            };
            // so this is gonna find the dtms id and should put whatever text in that node
            var dtmTmpl = _.template(this.$cache.dtmForm);
            // Append a dom node
            $(dtmTmpl(data)).appendTo(this.$el.find('#dtms')).hide().slideDown();

            this.$cache.dtmCount++;
        },

        removeDtm: function(e) {
            if (confirm("Are you sure you want to remove this date/time?")) {
                var dtmNumber = $(e.currentTarget).data('count');
                $('#dtm-' + dtmNumber).slideUp('slow', function() {
                    this.remove();
                });
            }
        },

        addLineItem: function() {
            var data = {
                count: this.$cache.lineItemCount
            };

            var lineItemTmpl = _.template(this.$cache.lineItemForm);
            $(lineItemTmpl(data)).appendTo(this.$el.find('#line-items')).hide().slideDown();

            this.$cache.lineItemCount++;
        },

        removeLineItem: function(e) {
            if (confirm("Are you sure you want to remove this line item?")) {
                var lineItemNumber = $(e.currentTarget).data('count');
                $('#line-item-' + lineItemNumber).slideUp('slow', function() {
                    this.remove();
                });
            }
        },

        addItemDtm: function(e) {
            var lineItemCount = $(e.currentTarget).data('count');
            var lineItemEl = '#item-dtms-' + lineItemCount;

            $(this.$el.find('#item-dtms-div-' + lineItemCount)).show();

            var data = {
                count: this.$cache.itemDtmCount,
                lineItemCount: lineItemCount
            };

            var itemDtmTmpl = _.template(this.$cache.itemDtmForm);
            $(itemDtmTmpl(data)).appendTo(this.$el.find(lineItemEl)).hide().slideDown();

            this.$cache.itemDtmCount++;
        },

        removeItemDtm: function(e) {
            var itemDtmNumber = $(e.currentTarget).data('count');
            var lineItemNumber = $(e.currentTarget).data('lineitem');

            if (confirm("Are you sure you want to remove this line item date?")) {
                $('#item-dtm-' + itemDtmNumber).slideUp('slow', function() {
                    this.remove();

                    // This checks if item-dtms-0 has children then hides its parent
                    // Inside the callback of hiding the dtm form
                    if ($('#item-dtms-' + lineItemNumber).is(':empty')) {
                        $('#item-dtms-div-' + lineItemNumber).slideUp();
                    }
                });
            }
        },

        createPo: function(e) {
            e.preventDefault();
            var self = this;

            if (this.model.set(this.getCurrentFormValues(), { validate: true })) {
                dataService.createPo(self.model).then(function(newPo) {
                    app.pos.add(newPo);
                    app.pos.get(newPo.ID);
                    Router.navigate('', { trigger: true });
                });
            } else {
                $('#errorMsg').text(this.model.validationError);
                $('#validationError').fadeIn('slow');
                $('#btnSave').removeClass('btn-default');
                $('#btnSave').addClass('btn-danger');
            }
        },

        closeError: function(e) {
            $('#validationError').slideUp();
        },

        resetError: function() {
            $('#btnSave').removeClass('btn-danger');
            $('#btnSave').addClass('btn-default');
        },

        back: function() {
            Backbone.history.history.back();
        },

        getCurrentFormValues: function() {
            var today = app.moment();
            today = today.add(today.utcOffset());

            var dtms = [];
            for (var j = 0; j < this.$cache.dtmCount; j++) {
                if ($('#dtm-' + j).is('html *')) {
                    var dtm = {
                        DTM01_DateTimeQualifier: $('#DTM01_DateTimeQualifier-' + j).val(),
                        DTM02_PurchaseOrderDate: $('#DTM02_PurchaseOrderDate-' + j).val()
                    };
                    dtms.push(dtm);
                }
            }

            var lineItems = [];
            // Loop through each possible lineitem
            for (var i = 0; i < this.$cache.lineItemCount; i++) {
                // Check that lineitem wasn't removed from the DOM
                if ($('#line-item-' + i).is('html *')) {

                    // Each Line Item
                    var item = {
                        PO102_QuantityOrdered: $('#PO102_QuantityOrdered-' + i).val(),
                        PO103_UnitOfMeasurement: $('#PO103_UnitOfMeasurement-' + i).val(),
                        PO104_UnitPrice: $('#PO104_UnitPrice-' + i).val(),
                        PO105_BasisOfUnitPriceCode: $('#PO105_BasisOfUnitPriceCode-' + i).val(),
                        PO106_ProductIdQualifier: $('#PO106_ProductIdQualifier-' + i).val(),
                        PO107_ProductID: $('#PO107_ProductID-' + i).val()
                    };

                    // Dtm for each Line Item
                    var itemDtms = [];
                    for (var l = 0; l < this.$cache.itemDtmCount; l++) {
                        // Checks that the dtm is a child of this lineitem
                        if ($('#item-dtms-' + i + ' > #item-dtm-' + l).is('html *')) {
                            var itemDtm = {
                                DTM01_DateTimeQualifier: $('#item-DTM01_DateTimeQualifier-' + l).val(),
                                DTM02_PurchaseOrderDate: $('#item-DTM02_PurchaseOrderDate-' + l).val()
                            };
                            itemDtms.push(itemDtm);
                        }
                    }

                    // Push Item Dtm, if any, to the individual line item
                    if (itemDtms.length > 0) {
                        item.Dtms = itemDtms;
                    }

                    // Push the Line Item to the array of Items
                    lineItems.push(item);
                }
            }

            var envelope = {
                "ISA01_AuthInfoQualifier": "00",
                "ISA02_AuthInfo": "          ",
                "ISA03_SecurityInfoQualifier": "00",
                "ISA04_SecurityInfo": "          ",
                //"ISA05_InterchangeSenderIdQualifier": "01",
                //"ISA06_InterchangeSenderId": "828513080      ",
                //"ISA07_InterchangeReceiverIdQualifier": "01",
                //"ISA08_InterchangeReceiverId": "001903202U     ",
                "ISA09_Date": today.format(),
                "ISA10_Time": today.format('HHmm'),
                "ISA11_InterchangeControlStandardsIdentifier": "U",
                "ISA12_InterchangeControlVersionNumber": "00401",
                "ISA13_InterchangeControlNumber": "000000000",
                "ISA14_AcknowledgmentRequested": "0",
                "ISA15_UsageIndicator": "P",
                "ISA16_ComponentElementSeparator": "~",
                "IEA01_NumberOfIncludedFunctionalGroups": "1",
                "IEA02_InterchangeControlNumber": "000000000",
                "GS01_FunctionalIdentifierCode": "PO",
                //"GS02_ApplicationSenderCode": "828513080",
                //"GS03_ApplicationReceiverCode": "001903202U",
                "GS04_Date": today.format(),
                "GS05_Time": today.format('HHmm'),
                "GS06_GroupControlNumber": "245",
                "GS07_ResponsibleAgencyCode": "X",
                "GS08_Version": "004010",
                "GE01_NumberOfTransactionSetsIncluded": "1",
                "GE02_GroupControlNumber": "245",
                "ST01_TransactionSetIdentifierCode": "850",
                "ST02_TransactionSetControlNumber": "0001",
                //"SE01_NumberOfIncludedSegments": "36",
                "SE02_TransactionSetControlNumber": "0001"
            };

            return {
                BEG01_TransactionSetPurposeCode: "00",
                BEG02_PurchaseOrderTypeCode: "SA",
                BEG03_PurchaseOrderNumber: "8888",
                BEG05_Date: today.format(),
                CUR01_CurrencyEntityIdentifierCode: "SE",
                CUR02_CurrencyCode: "USD",
                CTT01_NumberofLineItems: lineItems.length,
                PoEnvelope: envelope,
                Items: lineItems,
                Dtms: dtms
            };
        }
    });

    return poCreateView;
});