var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('angular2/core');
var LevelComponent = (function () {
    function LevelComponent() {
        this.level = {
            id: 1,
            background: "",
            title: "Level 1",
            seconds: 60
        };
        this.timeStarted = false;
    }
    LevelComponent.prototype.ngOnInit = function () {
        this.startTime();
    };
    LevelComponent.prototype.startTime = function () {
        var _this = this;
        if (!this.timeStarted) {
            var clock = setInterval(function () {
                _this.level.seconds--;
                if (_this.level.seconds == 0) {
                    clearInterval(clock);
                }
            }, 1000);
            this.timeStarted = true;
        }
    };
    LevelComponent = __decorate([
        core_1.Component({
            selector: "level",
            template: "\n        <h1>{{level.title}}</h1>\n        <p>Time left: <b>{{level.seconds}}</b></p>\n        \n    ",
        }), 
        __metadata('design:paramtypes', [])
    ], LevelComponent);
    return LevelComponent;
})();
exports.LevelComponent = LevelComponent;
//# sourceMappingURL=level.component.js.map