const crypto = require('crypto');
const encryptionType = 'aes-256-cbc';
const encryptionEncoding = 'base64';
const bufferEncryption = 'utf-8';
const AesKey = "12345678123456781234567812345678";
const AesIV = "ABCDEFGHIJKLMNOP";

function encrypt(jsonObject) {
    const val = JSON.stringify(jsonObject);
    const key = Buffer.from(AesKey, bufferEncryption);
    const iv = Buffer.from(AesIV, bufferEncryption);
    const cipher = crypto.createCipheriv(encryptionType, key, iv);
    let encrypted = cipher.update(val, bufferEncryption, encryptionEncoding);
    encrypted += cipher.final(encryptionEncoding);
    return encrypted;
}

function decrypt(base64String) {
    const buff = Buffer.from(base64String, encryptionEncoding);
    const key = Buffer.from(AesKey, bufferEncryption);
    const iv = Buffer.from(AesIV, bufferEncryption);
    const decipher = crypto.createDecipheriv(encryptionType, key, iv);
    const deciphered = decipher.update(buff) + decipher.final();
    return JSON.parse(deciphered);
}

var fs = require('fs');
fs.readFile('D:/Personal/AESEncryption/AESEncryption/image.jpg', function (err, data) {
    if (err) throw err // Fail if the file can't be read.
    let base64Image = Buffer.from(data, 'binary').toString('base64');
    console.log(base64Image);
    let encryptedData = encrypt(base64Image);
    
    fs.writeFile('D:/Personal/AESEncryption/AESEncryption/imageEncryptedFromNode.enc', encryptedData, 'binary', function (err) {
        if (err)
            console.log(err);
        else
            console.log("The file was saved!");
    });
});
