module.exports = {
  name: 'resources',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/resources',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
