///<reference path="tests.config.js"/>
///<reference path="jasmine.js"/>

require(['mappers.imagesMapper'], function (mapper) {
    describe("With image mapper", function() {
        it("Should be able to map a JSON image feed to image objects", function() {
            var images = [{'caption': 'Lessmoke #girlswhosmokeweed #hightimes #weed #sour #igstoners #nuggettogether #rtrees', 'user': 'shannonxbebe', 'link': 'http://instagr.am/p/UjbBg3LtYP/', 'image_standard_res': 'http://distilleryimage7.s3.amazonaws.com/5fe8068c600411e2bf8022000a1fbe54_7.jpg', 'likes': 2 }, { 'caption': '#Sigh .. #lazy day.. lets get it started #Bludream #sour #clouds #instagood', 'user': 'nica_leek', 'link': 'http://instagr.am/p/UjahtujnTG/', 'image_standard_res': 'http://distilleryimage10.s3.amazonaws.com/c4a33502600311e2985c22000a1f9ad3_7.jpg', 'likes': 2 }, { 'caption': '...Just Not For Me Right Now...\n#Kush #Dank #Loud #Sour #Marijuana #MaryJane #Wfayo #Chillin #Coolin #ChicagoBound #Highsociety #Flysociety #RichLife #RealTalk #Stoned #Blown #Blasted #Blazed #Baked #Random #JayBanger #Moments #WhyNot #Pointless #Lmfao #FuckIt #NoTimeForGames #Deuces #Igfamous #HighLife', 'user': 'jaybangerchicago', 'link': 'http://instagr.am/p/UjZ_ibjd7v/', 'image_standard_res': 'http://distilleryimage11.s3.amazonaws.com/1dc2ca90600311e29a6422000a9e06c4_7.jpg', 'likes': 18 }]
            var mappedImages = mapper.map(images);

            expect(mappedImages.length).toBe(3);
            expect(mappedImages[0].user()).toEqual('shannonxbebe');
            expect(mappedImages[0].likes()).toEqual(2);
        });
    });
});

