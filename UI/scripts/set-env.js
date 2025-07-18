const fs = require("fs");
const path = require("path");

try {
  const env = process.env.ENV || "development";
  const apiUrl = process.env.API_URL || "";

  const targetPath = path.resolve(
    __dirname,
    `../src/environments/environment.${env}.ts`
  );

  const fileContent = `
export const environment = {
  production: ${env === "production"},
  apiBaseUrl: '${apiUrl}'
};
`;

  fs.writeFileSync(targetPath, fileContent);
  console.log(`✅ Environment settings written to environment.${env}.ts`);
} catch (error) {
  console.error("❌ Failed to write environment settings:", error);
  process.exit(1);
}
