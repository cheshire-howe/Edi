define([
    'jquery',
    'underscore',
    'backbone'],
function($, _, Backbone) {
    var appController = {
        currentView: null,

        pos: function() {
            var self = this;
            require(['views/posView'], function(PosView) {
                var view = new PosView();
                self.renderView.call(self, view);
            });
        },

        details: function(id) {
            var self = this;
            require(['views/detailsView'], function(DetailsView) {
                var po = app.pos.get(id),
                    view = new DetailsView({ model: po });
                self.renderView.call(self, view);
            });
        },

        renderView: function(view) {
            this.currentView && this.currentView.remove();
            $('#main').html(view.render().el);
            this.currentView = view;
        }
    };

    return appController;
});