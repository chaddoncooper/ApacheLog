module.exports = {
  name: 'apache-log-presentation',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/apache-log-presentation',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
