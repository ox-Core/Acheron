#include "system.hpp"
#include "ecs/types.hpp"

#include <print>
#include <cassert>
#include <queue>

using namespace acheron::ecs;

void SystemManager::EntityDespawned(Entity entity) {
    for(auto& [_, system] : systems) {
        system->entities.erase(entity);
    }
}

void SystemManager::EntitySignatureChanged(Entity entity, Signature signature) {
    for(auto& [typeName, system] : systems) {
        auto const systemSignature = signatures[typeName];

        auto it = signatures.find(typeName);
        if (it == signatures.end()) continue;

        const auto& sysSig = it->second;
        bool match = true;

        for (auto comp : sysSig) {
            if (signature.find(comp) == signature.end()) {
                match = false;
                break;
            }
        }

        if (match) system->entities.insert(entity);
        else       system->entities.erase(entity);
    }
}

SystemStageID SystemManager::GetStageOrFail(const std::string& name) {
    auto it = nameToID.find(name);
    if (it != nameToID.end()) {
        return it->second;
    }

    throw std::runtime_error("Stage '" + name + "' does not exist");
}

SystemStageID SystemManager::GetOrCreateStage(const std::string& name) {
    auto it = nameToID.find(name);
    if (it != nameToID.end()) {
        return it->second;
    }

    SystemStageID id = ++counter;
    nameToID[name] = id;
    stages.push_back({id, name});
    return id;
}

void SystemManager::StageBefore(std::string name, std::string after) {
    auto nameID  = GetOrCreateStage(name);
    auto afterID = GetStageOrFail(after);

    edges[nameID].push_back(afterID);
}

void SystemManager::StageAfter(std::string name, std::string before) {
    auto nameID  = GetOrCreateStage(name);
    auto beforeID = GetStageOrFail(before);
    edges[beforeID].push_back(nameID);
}

void SystemManager::PopulateStages() {
    std::unordered_map<SystemStageID, int> indegree;

    for (auto& [id, _] : stages) {
        indegree[id] = 0;
    }

    for (auto& [from, tos] : edges) {
        for (auto to : tos) {
            indegree[to]++;
        }
    }

    std::queue<SystemStageID> q;
    for (auto& [id, deg] : indegree) {
        if (deg == 0) q.push(id);
    }

    orderedStages.clear();

    while (!q.empty()) {
        auto u = q.front(); q.pop();
        orderedStages.push_back(u);

        for (auto v : edges[u]) {
            indegree[v]--;
            if (indegree[v] == 0) {
                q.push(v);
            }
        }
    }

    if (stages.size() != orderedStages.size()) {
        throw std::runtime_error("Cycle detected in system stages");
    }
}
