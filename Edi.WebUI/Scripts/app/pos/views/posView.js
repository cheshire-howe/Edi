define([
    'jquery',
    'underscore',
    'backbone',
    'views/poView',
    'routers/router'
],
function($, _, Backbone, PoView, Router) {
    var posView = Backbone.View.extend({
        tagName: 'div',

        initialize: function() {
            var self = this;
            this.collection = app.pos;
            this.collection.fetch();
            this.collection.bind('add remove', this.onModelAddedOrRemoved, this);
        },

        onModelAddedOrRemoved: function() {
            this.render();
        },

        render: function() {
            this.$el.html("<h2>Purchase Orders</h2>");
            this.$el.append("<p><a href='#/create'>Create Purchase Order</a></p>");
            this.collection.each(function(item) {
                this.addOne(item);
            }, this);
            return this;
        },

        addOne: function(po) {
            var view = new PoView({ model: po });
            this.$el.append(view.render().el);
        }
    });

    return posView;
});