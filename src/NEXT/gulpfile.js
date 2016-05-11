/// <binding BeforeBuild='devBuild' Clean='cleanDocs, cleanDist' />
var gulp = require('gulp'),
    watch = require('gulp-watch'),
    util = require('gulp-util'),
    changed = require('gulp-changed'),
    sequence = require('run-sequence'),
    injectReload = require('gulp-inject-reload'),
    lr = require('gulp-livereload'),
    bower = require('gulp-bower'),
    cookieParser = require('cookie-parser'),
    express = require('express'),
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


gulp.task('devBuild', ['components'], function () {
    var liveReloadConfig = {
        port: LIVERELOAD_PORT,
        host: 'http://127.0.0.1',
        script: 'livereload.js'
    };
    gulp.src([SRC + '**/*.*'])
        .pipe(changed(DIST))
        .pipe(gulp.dest(DIST));
    gulp.src([SRC + 'index.js'])
        .pipe(changed(DIST + 'app'))
        .pipe(gulp.dest(DIST));
    return gulp.src(SRC + 'index.html')
        //.pipe(injectReload(liveReloadConfig))
        .pipe(gulp.dest(DIST));
})


gulp.task('components', function() {
    return sequence(['copyCSS', 'copyImages', 'copyFonts', 'copycomponents']);
});


gulp.task('copycomponents',
    function() {
        return gulp.src([
                ASSET + 'bower/angular/angular.min.js',
                ASSET + 'bower/angular-localization/angular-localization.js',
                ASSET + 'bower/angular-sanitize/angular-sanitize.min.js',
                ASSET + 'bower/underscore/underscore-min.js',
                ASSET + 'bower/angular-ui-grid/ui-grid.min.js',
                ASSET + 'bower/angular-ui-grid/ui-grid.js',
                ASSET + 'bower/angular-toArrayFilter/toArrayFilter.js',
                ASSET + 'bower/angular-animate/angular-animate.min.js',
                ASSET + 'bower/angular-resource/angular-resource.min.js',
                ASSET + 'bower/angular-cookies/angular-cookies.min.js',
                ASSET + 'bower/angular-route/angular-route.min.js',
                ASSET + 'bower/angular-touch/angular-touch.min.js',
                ASSET + 'bower/livereload/dist/livereload.js',
                ASSET + 'bower/angular-mocks/angular-mocks.js',
                ASSET + 'bower/angular-ui-router/release/angular-ui-router.min.js',
                ASSET + 'bower/angular-bootstrap/ui-bootstrap-tpls.min.js',
                ASSET + 'bower/oclazyload/dist/ocLazyLoad.min.js',
                '/node_modules/quagga/dist/quagga.min.js'

            ])
            .pipe(changed(DIST + 'assets/js/'))
            .pipe(gulp.dest(DIST + 'assets/js/'));
    });


gulp.task('copyCSS', function() {
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


function reloadCB(event) {
    var fileName = require('path').relative(__dirname + "/" +
      DIST, event.path);
    console.log(fileName);

    lr.changed({
        body: {
            files: [fileName]
        }
    });
};


gulp.task('cleanDocs', function(){
    return gulp.src([DOC + '**/*.*', DOC + '*.*'], {read:false}).pipe(clean());
});


gulp.task('cleanDist', function () {
    return gulp.src([DIST + '**/*.*', DIST + '*.*', DIST], { read: false }).pipe(clean());
});

gulp.task('buildWatcher', function() {
    util.log('watching ' + SRC + ' for build');
    return gulp.watch([SRC + '**/*.*', SRC + '*.*', '!' + SRC +
        '/assets/bower/**/*.*', excludeBowerImports[1]
    ], [
        'devBuild', 'docs'
    ]);
});


gulp.task('reloadWatcher', function () {
    util.log('watching ' + DIST + 'for builds');
    return gulp.watch([DIST + '**/*.*', DIST +
        '*.*'
    ], lr.event);

});


gulp.task('devEnv', function (cb) {
    lr.listen();
    console.log(lr)
    return sequence( 'devBuild', [
        'buildWatcher', 'reloadWatcher'
    ]);
});
