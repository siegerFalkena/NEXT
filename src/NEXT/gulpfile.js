var gulp = require('gulp'),
    watch = require('gulp-watch'),
    util = require('gulp-util'),
    changed = require('gulp-changed'),
    sequence = require('run-sequence'),
    injectReload = require('gulp-inject-reload'),
    bower = require('gulp-bower'),
    cookieParser = require('cookie-parser'),
    Server = require('karma').Server,
    clean = require('gulp-clean'),
    shell = require('gulp-shell');


var SRC = './app/',
    src = SRC.substring(2, 100),
    DIST = './wwwroot/',
    dist = DIST.substring(2, 100),
    ASSET = './assets/',
    asset = ASSET.substring(2, 100),
    DOC = './documentation/',
    doc = DOC.substring(2, 100),
    PORT = 4000,
    LIVERELOAD_PORT = 35729;
//build, components

var excludeBowerImports = ['!' + src + 'assets/bower/*.*',
    '!' + src + 'assets/bower/**/*.*'
];


gulp.task('docs', function(){
    return sequence('cleanDocs', 'generateDocs');
});


gulp.task('build', ['components', 'docs'], function() {
    util.log(excludeBowerImports);
    gulp.src([SRC + '**/*.*'
        ])
        .pipe(changed(DIST))
        .pipe(gulp.dest(DIST));
    return gulp.src([SRC + '*.*', '!' + SRC +
            'assets/*.*', '!**/*.css'
        ])
        .pipe(changed(DIST))
        .pipe(gulp.dest(DIST));
})


gulp.task('devBuild', ['components', 'test'], function() {
    gulp.src([SRC + '**/*.*'])
        .pipe(changed(DIST))
        .pipe(gulp.dest(DIST));
    gulp.src([SRC + 'index.js'])
        .pipe(changed(DIST + 'app'))
        .pipe(gulp.dest(DIST));
    return gulp.src(SRC + 'index.html')
        .pipe(injectReload({
            port: LIVERELOAD_PORT,
            host: 'http://" + (location.host || "localhost").split(":")[0] + "',
            script: 'livereload.js',
            snipver: 1
        }))
        .pipe(gulp.dest(DIST));
})


gulp.task('components', function() {
    return sequence('bower', 'copycomponents');
});


gulp.task('copycomponents', ['copyCSS', 'copyImages'],
    function() {
        return gulp.src([
                ASSET + 'bower/angular/angular.min.js',
                ASSET + 'bower/angular-localization/angular-localization.js',
                ASSET + 'bower/angular-sanitize/angular-sanitize.min.js',
                ASSET + 'bower/underscore/underscore-min.js',
                ASSET + 'bower/angular-ui-grid/ui-grid.min.js',
                ASSET + 'bower/angular-toArrayFilter/toArrayFilter.js',
                ASSET + 'bower/angular-animate/angular-animate.min.js',
                ASSET + 'bower/angular-resource/angular-resource.min.js',
                ASSET + 'bower/angular-cookies/angular-cookies.min.js',
                ASSET + 'bower/angular-route/angular-route.min.js',
                ASSET + 'bower/angular-mocks/angular-mocks.js',
                ASSET + 'bower/angular-ui-router/release/angular-ui-router.min.js',
                ASSET + 'bower/angular-bootstrap/ui-bootstrap-tpls.min.js',
                ASSET + 'bower/oclazyload/dist/ocLazyLoad.min.js',
                '/node_modules/quagga/dist/quagga.min.js'

            ])
            .pipe(changed(DIST + 'assets/js/'))
            .pipe(gulp.dest(DIST + 'assets/js/'));
    });


gulp.task('copyCSS', ['copyFonts'], function() {
    return gulp.src([
            ASSET + 'css/metro-bootstrap.css',
            ASSET + 'css/flags.css',
            ASSET + 'css/ui-grid.css',
            ASSET + 'bower/bootstrap/dist/css/bootstrap.min.css',
            ASSET + 'bower/angular-ui-grid/ui-grid.woff',
            ASSET + 'bower/angular-ui-grid/ui-grid.ttf',
            ASSET + 'bower/angular-ui-grid/ui-grid.min.css',
            ASSET + 'bower/angular-ui-grid/ui-grid.svg',
            ASSET + 'bower/angular-ui-grid/ui-grid.eot',
            ASSET + 'css/stylesheet.css'
        ])
        .pipe(changed(DIST + 'assets/css/'))
        .pipe(gulp.dest(DIST + 'assets/css/'));
});

gulp.task('copyFonts', function() {
    return gulp.src([
            ASSET + 'fonts/*.*'
        ])
        .pipe(changed(DIST + 'assets/fonts/'))
        .pipe(gulp.dest(DIST + 'assets/fonts/'));
});

gulp.task('copyImages', function() {
    return gulp.src([
            ASSET + 'img/*.*'
        ])
        .pipe(changed(DIST + 'assets/img/'))
        .pipe(gulp.dest(DIST + 'assets/img/'));
});

gulp.task('bower', function() {
    return bower({
        cmd: 'install'
    });
});

var child_exec = require('child_process').exec;
gulp.task('generateDocs', function(done) {
    child_exec('node ./node_modules/jsdoc/jsdoc.js -r -d ./documentation -c jsdoc.conf', undefined, done);
});


var lr;
function reloadCB(event) {
    var fileName = require('path').relative(__dirname +
        '/dist/', event.path);

    lr.changed({
        body: {
            files: [fileName]
        }
    });
};


gulp.task('cleanDocs', function(){
    return gulp.src([DOC + '**/*.*', DOC + '*.*'], {read:false}).pipe(clean());
});


gulp.task('test', function() {
    gulp.src(
        'assets/bower/angular-mocks/angular-mocks.js'
    ).pipe(gulp.dest('assets/js/'));
    return sequence('devBuild', 'runTestServer');
});


gulp.task('runTestServer', function(done) {
    new Server({
        configFile: __dirname +
            '/karma.conf.js',
        singleRun: true
    }, function() {
        done();
    }).start();
});


gulp.task('buildWatcher', function() {
    util.log('watching ' + SRC + ' for build');
    return gulp.watch([SRC + '**/*.*', SRC + '*.*', '!' + SRC +
        '/assets/bower/**/*.*', excludeBowerImports[1]
    ], [
        'devBuild', 'docs'
    ]);
});


gulp.task('reloadWatcher', function() {
    util.log('watching ' + DIST + 'for builds');
    return gulp.watch([DIST + '**/*.*', DIST +
        '*.*'
    ], reloadCB);
});


gulp.task('devEnv', function() {
    return sequence( 'devBuild', [
        'buildWatcher', 'reloadWatcher'
    ]);
});
