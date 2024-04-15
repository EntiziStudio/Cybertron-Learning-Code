module.exports = {
  testEnvironment: "jsdom",
  moduleNameMapper: {
    "^@/(.*)$": "./components/$1",
  },
  transform: {
    "^.+\\.(js|jsx)$": "babel-jest",
  },
  testPathIgnorePatterns: ["/node_modules/", "/.next/"],
  moduleFileExtensions: ["js", "jsx"],
  setupFilesAfterEnv: ["./setupTests.js"],
};
