module.exports = {
  name: 'apache',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/apache',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
