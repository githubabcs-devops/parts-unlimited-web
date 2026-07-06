#!/usr/bin/env node
import fs from 'node:fs';
import path from 'node:path';
import process from 'node:process';
import { fileURLToPath } from 'node:url';

/**
 * Deterministic "coder" step — the OFFLINE FALLBACK for this repo's agentic SDLC workflow
 * (`.github/workflows/agentic-sdlc.yml`, mode: deterministic).
 *
 * The default (real) path assigns the work item to the GitHub Copilot coding agent, which branches,
 * implements, tests, and opens a PR autonomously. This script is used only by the `deterministic` mode:
 * a repeatable, network-free change so a rehearsal never depends on Copilot availability. It edits the
 * app in `src/Web`: it adds a home-page highlight derived from the work item. Tests in `src/Web.Tests`
 * cover the change, and the resulting PR is reviewed by the installed PR-review app.
 *
 * Located at `.github/agentic/` (two levels below the repo root), so `repoRoot` resolves to the repo root.
 */

const scriptDir = path.dirname(fileURLToPath(import.meta.url));
const repoRoot = path.resolve(scriptDir, '..', '..');
const homeContent = path.join(repoRoot, 'src', 'Web', 'HomeContent.cs');

const workItemId = process.env.WORK_ITEM_ID || process.argv[2] || 'demo';
const workItemTitle =
  process.env.WORK_ITEM_TITLE || process.argv.slice(3).join(' ') || 'Agentic SDLC demo story';

let original;
try {
  original = fs.readFileSync(homeContent, 'utf8');
} catch (err) {
  if (err && err.code === 'ENOENT') {
    console.error(`Expected sample app not found at ${path.relative(repoRoot, homeContent)}`);
    process.exit(1);
  }
  throw err;
}

// Build a safe, single-line C# string literal from the work item.
const highlight = `AB#${workItemId}: ${workItemTitle}`
  .replace(/"/g, "'")
  .replace(/\r?\n/g, ' ')
  .trim();

if (original.includes(highlight)) {
  console.log(`No change needed; highlight already present for AB#${workItemId}.`);
  process.exit(0);
}

// Insert the new highlight as the first item of the Highlights initializer.
const marker = /(Highlights\s*=\s*new\[\]\s*\r?\n\s*\{\r?\n)/;
if (!marker.test(original)) {
  console.error('Could not locate the Highlights initializer in src/Web/HomeContent.cs.');
  process.exit(1);
}

const indent = '        ';
const updated = original.replace(marker, `$1${indent}"${highlight}",\n`);
fs.writeFileSync(homeContent, updated, 'utf8');

console.log(`Agentic SDLC edit applied for AB#${workItemId}: ${workItemTitle}`);
console.log(`Modified ${path.relative(repoRoot, homeContent)} (added a home-page highlight).`);
