/// <reference path="jquery-1.5.1.js" />

jQuery.noConflict();

kanga = function (apiBaseUri) {

  var _apiBaseUri = apiBaseUri;

  var _client = function () {

    this.createDiagram = function (arguments, callback) {

      arguments = { source: encodeURI(arguments.source), type: arguments.type, style: arguments.style };

      jQuery.get(_apiBaseUri + '/create', arguments, callback, 'json');

    }

  }

  var _highlighter = function (client) {

    var _client = client;
    var _this = this;

    _this.replaceAll = function () {

      jQuery('pre.kanga').each(function (i, element) {

        _this.replace(element);

      });

    }

    _this.replace = function (element) {

      var source = jQuery(element).text();
      var type = jQuery(element).data('kanga-type');
      var style = jQuery(element).data('kanga-style');

      var arguments = { source: source, type: type, style: style };

      _client.createDiagram(arguments, function (result) {

        var information = buildInformation(result);
        var img = jQuery('<img></img>')
          .attr('src', result.diagram)
          .attr('title', information)
          .attr('alt', information)
          .css('display', 'block');

        jQuery(element).after(img);
        jQuery(element).hide();

      });

    }

    var buildInformation = function (result) {

      var information = '';
      information += 'Source:\r\n';
      information += result.source;

      if (result.errors.length > 0) {

        information += '\r\n';
        information += 'Errors:\r\n';

        jQuery(result.errors).each(function (index, error) {

          information += error.message;
          information += '(';
          information += 'line ' + error.token.line;
          information += ', start ' + error.token.start;
          information += ', end ' + error.token.end;
          information += ', value ' + error.token.value;
          information += ')';
          information += '\r\n';

        });

      } else {

        information += '\r\n';
        information += 'No errors';

      }

      return information;

    }
  }

  this.client = new _client();

  this.highlighter = new _highlighter(this.client);

}

var kanga = new kanga('_KANGA_API_BASE_URI_');

jQuery(document).ready(function () {

  kanga.highlighter.replaceAll();

});