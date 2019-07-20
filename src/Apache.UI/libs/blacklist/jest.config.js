module.exports = {
  name: 'blacklist',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/blacklist',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
