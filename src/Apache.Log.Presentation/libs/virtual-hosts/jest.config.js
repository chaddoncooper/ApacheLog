module.exports = {
  name: 'virtual-hosts',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/virtual-hosts',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
