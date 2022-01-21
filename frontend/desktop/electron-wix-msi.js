const path = require('path');
const { MSICreator } = require('electron-wix-msi');

const msiCreator = new MSICreator({
	appDirectory: path.resolve(__dirname,".","release-builds","Fuar_Otomasyon-win32-ia32"),
  description: "Fuar Otomasyon Uygulaması",
  exe: "Fuar_Otomasyon",
  name: "Fuar Otomasyon",
  manufacturer: "CSoft",
  version: "1.0.0",
  outputDirectory: path.resolve(__dirname,".","release-builds"),
  cultures: 'tr-TR',
  language: 1155
});

async function build() {
	await msiCreator.create();
	await msiCreator.compile();
}

build().catch(console.error);