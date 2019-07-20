module.exports = {
  name: 'management',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/management',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
