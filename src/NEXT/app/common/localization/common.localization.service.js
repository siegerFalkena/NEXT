angular.module('common.localization')
    .factory('l10n', ['$locale', '$http', '$log', '$cookies', '$rootScope', l10nFact]);

/**
 * initializes a JSON locale object in scope. locale object contains identifier <locale> and bindings <english, translated> for translations
 * Also persists language in the scope
 *
 * @method     init
 * @param      {$scope}    scope              scope object to create locale object in
 * @param      {Function}  onChangeFunctions  callback function without arguments for retranslating controller generated text 
 * @param      {Object}  [specificLanguage] languageObject akin to supportedLanguage format 
 */
function l10nFact($locale, $http, $log, $cookies, $rootScope) {
    var l10nService = {};
    l10nService.supportedLanguages = [
        { name: 'nl', id: 'nl', flag: 'src', file: '/language/nl-nl.json' },
        { name: 'gb', id: 'gb', flag: 'src', file: '/language/en-gb.json' }
    ];
    l10nService.currentLocale = l10nService.supportedLanguages[1];
    l10nService.localefile = undefined;

    l10nService.getLocale = function getLocale() {
        return l10nService.localefile;
    }

    l10nService.getLocalized = function getLocalizedF(symbol){
        var implementation = l10nService.localefile[symbol]
        return implementation ? implementation : "l10n."+symbol;
    }


    l10nService.init = function init(specificLang) {
        var cookieLang = $cookies.get('locale')
        if (cookieLang !== undefined) {
            l10nService.currentLocale = angular.fromJson(cookieLang)
        } else {
            l10nService.currentLocale = l10nService.supportedLanguages[1]
        };
        
        if (specificLang !== undefined) {
            l10nService.currentLocale = specificLang;
        };

        function cb_success(res) {
            var data = res.data;
            l10nService.localefile = data;
            $cookies.put('locale', angular.toJson(l10nService.currentLocale));
        };

        function cb_failure(res) {
            $log.info('failed getting localization file: ' + res.status + "\t" + res.statusText);
        };

        $http.get(l10nService.currentLocale.file, {}).then(cb_success, cb_failure);
    };

    return {
        getLocale: function() {
            return l10nService.getLocale()
        },

        init: function(langMetaObject) {
            return l10nService.init(langMetaObject)
        },
        currentLocale: function() {
            return l10nService.currentLocale
        },

        getLocalized: l10nService.getLocalized,
        supportedLanguages: function() {
            return l10nService.supportedLanguages
        },

        localeFile: l10nService.localefile

    };
};
