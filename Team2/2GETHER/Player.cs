namespace _2GETHER
{
    class Player
    {
        public string Name { get; private set; } // 플레이어 이름
        public int Level { get; private set; } // 플레이어 레벨
        public double Attack { get; private set; } // 플레이어 공격력
        public double Defense { get; private set; } // 플레이어 방어력
        public double Hp { get; private set; } // 현재 체력
        public double MaxHp { get; private set; } // 최대 체력
        public double Mp { get; private set; } // 현재 마나
        public double MaxMp { get; private set; } // 최대 마나
        public int Gold { get; private set; } // 소지 금화
        public int Exp { get; private set; } // 현재 경험치
        public int MaxExp { get; private set; } // 최대 경험치
        public EJob Job { get; private set; } // 플레이어 직업
        public int Potions { get; private set; } // 남은 포션 수
        public int MonsterKills { get; private set; } // 처치한 몬스터 수

        private const int MaxLevel = 5; // 최대 레벨

        private Random random = new Random(); // 랜덤 객체 생성

        public EquipmentItem[] WeaponEquipment = new EquipmentItem[1]; // 무기 장비 배열
        public EquipmentItem[] ArmorEquipment = new EquipmentItem[1]; // 방어구 장비 배열
        public List<EquipmentItem> equipmentInventory = new List<EquipmentItem>(); // 장비 아이템 인벤토리
        public List<ConsumableItem> consumableInventory = new List<ConsumableItem>(); // 소비 아이템 인벤토리

        List<Monster> AttackedMonster = new List<Monster>(); // 공격한 몬스터 목록

        public Player()
        {
            Name = "Chad"; // 기본 이름
            Level = 1; // 시작 레벨
            Attack = 10.0; // 기본 공격력
            Defense = 5.0; // 기본 방어력
            Hp = 100; // 기본 체력
            MaxHp = 100; // 최대 체력
            Mp = 50; // 기본 마나
            MaxMp = 50; // 최대 마나
            Gold = 5000; // 기본 금화
            Exp = 0; // 초기 경험치
            MaxExp = 100; // 최대 경험치
            Job = EJob.전사; // 기본 직업
            Potions = 3; // 기본 포션 수
            MonsterKills = 0; // 초기 몬스터 처치 수
        }

        public Player(string name, EJob job)
        {
            Name = name; // 사용자 지정 이름
            Job = job; // 사용자 지정 직업
            Level = 1; // 시작 레벨
            Attack = 10.0; // 기본 공격력
            Defense = 5.0; // 기본 방어력
            Hp = 100; // 기본 체력
            MaxHp = 100; // 최대 체력
            Mp = 50; // 기본 마나
            MaxMp = 50; // 최대 마나
            Gold = 0; // 초기 금화
            Exp = 0; // 초기 경험치
            MaxExp = 100; // 최대 경험치
            Potions = 3; // 기본 포션 수
            MonsterKills = 0; // 초기 몬스터 처치 수
        }

        // 회피, 크리티컬 공격
        public double AttackWithEffects()
        {
            if (random.NextDouble() <= 0.1) // 10% 확률로 공격 실패
            {
                return 0; // 데미지 0 반환
            }

            double damage = Attack; // 기본 데미지

            if (random.NextDouble() <= 0.15) // 15% 확률로 크리티컬 히트
            {
                damage *= 1.6; // 크리티컬 데미지 1.6배
            }

            return damage; // 최종 데미지 반환
        }

        // 플레이어 공격
        public double PlayerAttack(Monster monster)
        {
            double damage = AttackWithEffects(); // 효과가 있는 공격으로 데미지 계산

            double finalDamage = monster.MonsterDamageTaken(damage); // 몬스터가 입는 데미지 계산

            return finalDamage; // 몬스터가 입은 최종 데미지 반환
        }

        // 플레이어가 입은 데미지 처리
        public void PlayerDamageTaken(double damage)
        {
            Hp -= damage; // 체력 차감

            if (Hp < 0) Hp = 0; // 체력이 0 미만일 경우 0으로 설정
        }

        // 스킬 1 사용
        public double UseSkillOne(Monster monster)
        {
            if (Mp >= 10) // 마나가 10 이상일 경우
            {
                Mp -= 10; // 마나 차감
                double damage = Attack * 2; // 스킬 데미지 계산

                monster.MonsterDamageTaken(damage); // 몬스터가 입는 데미지 계산
                return damage; // 데미지 반환
            }
            else
            {
                Console.WriteLine("MP가 부족합니다!"); // 마나 부족 메시지 출력
                return 0; // 0 반환
            }
        }

        // 스킬 2 사용
        public double UseSkillTwo(List<Monster> monsters)
        {
            if (Mp >= 15) // 마나가 15 이상일 경우
            {
                Mp -= 15; // 마나 차감
                double damage = Attack * 1.5; // 스킬 데미지 계산

                if (monsters.Count == 1) // 몬스터가 1마리일 경우
                {
                    monsters[0].MonsterDamageTaken(damage); // 해당 몬스터에게 데미지 입힘
                    AttackedMonster.Add(monsters[0]); // 공격한 몬스터 목록에 추가
                    return damage; // 데미지 반환
                }
                else // 몬스터가 2마리 이상일 경우
                {
                    int firstMonster = random.Next(monsters.Count); // 첫 번째 몬스터 선택
                    int secondMonster;

                    do
                    {
                        secondMonster = random.Next(monsters.Count); // 두 번째 몬스터 선택
                    } while (secondMonster == firstMonster); // 첫 번째 몬스터와 중복되지 않도록

                    Monster firstTarget = monsters[firstMonster]; // 첫 번째 몬스터
                    Monster secondTarget = monsters[secondMonster]; // 두 번째 몬스터

                    AttackedMonster.Add(firstTarget); // 공격한 몬스터 목록에 추가
                    AttackedMonster.Add(secondTarget); // 공격한 몬스터 목록에 추가

                    firstTarget.MonsterDamageTaken(damage); // 첫 번째 몬스터에게 데미지 입힘
                    secondTarget.MonsterDamageTaken(damage); // 두 번째 몬스터에게 데미지 입힘
                    return damage; // 데미지 반환
                }
            }
            else
            {
                Console.WriteLine("MP가 부족합니다!"); // 마나 부족 메시지 출력
                return 0; // 0 반환
            }
        }

        // 공격한 몬스터 목록 반환
        public List<Monster> GetAttackedMonster()
        {
            return AttackedMonster; // 공격한 몬스터 목록 반환
        }

        // 스킬 1 이름 반환
        public string GetSkillNameOne()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "강타 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n"; // 전사 스킬 설명

                case EJob.마법사:
                    return "화염구 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n"; // 마법사 스킬 설명

                case EJob.궁수:
                    return "속사 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n"; // 궁수 스킬 설명

                default:
                    return "기본 공격"; // 기본 공격 반환
            }
        }

        // 스킬 2 이름 반환
        public string GetSkillNameTwo()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "폭풍 가르기 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n"; // 전사 스킬 설명

                case EJob.마법사:
                    return "얼음 폭풍 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n"; // 마법사 스킬 설명

                case EJob.궁수:
                    return "연속 사격 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n"; // 궁수 스킬 설명

                default:
                    return "기본 공격"; // 기본 공격 반환
            }
        }

        // 경험치 획득
        public int GainExp(int gainExp)
        {
            if (Level >= MaxLevel) // 최대 레벨에 도달한 경우
            {
                Console.WriteLine("최대 레벨에 도달하여 더 이상 경험치를 얻을 수 없습니다."); // 메시지 출력
                Console.ReadKey(true); // 키 입력 대기
                return 0; // 0 반환
            }

            Exp += gainExp; // 경험치 추가

            while (Exp >= MaxExp && Level < MaxLevel) // 경험치가 최대 경험치를 초과한 경우
            {
                LevelUp(); // 레벨업 처리
            }

            return Exp; // 현재 경험치 반환
        }

        // 몬스터 처치 시 레벨 시스템 처리
        public void LevelSystem(Monster monster)
        {
            int gainExp = monster.Level * 1; // 몬스터 레벨에 따라 경험치 계산

            GainExp(gainExp); // 경험치 획득
        }

        // 레벨업 처리
        private void LevelUp()
        {
            Exp -= MaxExp; // 경험치 차감
            Level++; // 레벨 증가

            switch (Level)
            {
                case 1:
                    MaxExp = 10; // 1레벨 최대 경험치
                    break;

                case 2:
                    MaxExp = 35; // 2레벨 최대 경험치
                    break;

                case 3:
                    MaxExp = 65; // 3레벨 최대 경험치
                    break;

                case 4:
                    MaxExp = 100; // 4레벨 최대 경험치
                    break;

                case 5:
                    MaxExp = 0; // 최대 레벨 도달
                    Console.WriteLine("축하드립니다! 최대 레벨에 도달했습니다!"); // 메시지 출력
                    Console.ReadKey(true); // 키 입력 대기
                    break;
            }

            Attack += 0.5; // 공격력 증가
            Defense += 1.0; // 방어력 증가
            MaxHp += 10.0; // 최대 체력 증가
            MaxMp += 10.0; // 최대 마나 증가

            Hp = MaxHp; // 현재 체력을 최대 체력으로 설정
            Mp = MaxMp; // 현재 마나를 최대 마나로 설정

            Console.WriteLine("레벨업하셨습니다! 축하드립니다!"); // 레벨업 메시지 출력
            Console.ReadKey(true); // 키 입력 대기
        }

        // 포션 사용
        public void UsePotion(ConsumableItem consumableItem)
        {
            if (Potions > 0) // 포션이 남아있는 경우
            {
                Hp += 30; // 체력 회복

                if (Hp > MaxHp) // 최대 체력을 초과한 경우
                {
                    Hp = MaxHp; // 최대 체력으로 설정
                }

                Potions--; // 포션 개수 차감
                consumableItem.RemoveCount(); // 포션 카운트 차감
                Console.WriteLine("회복을 완료했습니다. 현재 체력: {0}/{1}", Hp, MaxHp); // 회복 메시지 출력
                Console.ReadKey(true); // 키 입력 대기
            }
            else
            {
                Console.WriteLine("포션이 부족합니다."); // 포션 부족 메시지 출력
                Console.ReadKey(true); // 키 입력 대기
            }
        }

        // 포션 갯수 증가
        public void AddPotion(ConsumableItem consumableItem)
        {
            Potions++;
        }

        // 아이템 장착 시 능력치 업데이트
        public void UpdateStatsOnEquip(Item item)
        {
            Attack += item.ItemATK; // 공격력 증가
            Defense += item.ItemDEF; // 방어력 증가
        }

        // 아이템 해제 시 능력치 업데이트
        public void UpdateStatsOnUnequip(Item item)
        {
            Attack -= item.ItemATK; // 공격력 감소
            Defense -= item.ItemDEF; // 방어력 감소
        }

        // 아이템 장착
        public void EquipItem(int inputNum, Inventory inventory)
        {
            if (inputNum < 1 || inputNum > equipmentInventory.Count) // 유효하지 않은 아이템 번호
            {
                Console.WriteLine("유효하지 않은 아이템 번호입니다."); // 메시지 출력
                return; // 종료
            }

            EquipmentItem equipmentItem = equipmentInventory[inputNum - 1]; // 장착할 아이템 선택

            if (equipmentItem.eItemType == EItemType.Armor) // 방어구일 경우
            {
                if (ArmorEquipment[0] != null) // 기존 방어구가 있을 경우
                {
                    UpdateStatsOnUnequip(ArmorEquipment[0]); // 기존 방어구의 능력치 감소
                }

                ArmorEquipment[0] = equipmentItem; // 방어구 장착
                UpdateStatsOnEquip(equipmentItem); // 장착한 방어구의 능력치 증가
            }
            else if (equipmentItem.eItemType == EItemType.Weapon) // 무기일 경우
            {
                if (WeaponEquipment[0] != null) // 기존 무기가 있을 경우
                {
                    UpdateStatsOnUnequip(WeaponEquipment[0]); // 기존 무기의 능력치 감소
                }

                WeaponEquipment[0] = equipmentItem; // 무기 장착
                UpdateStatsOnEquip(equipmentItem); // 장착한 무기의 능력치 증가
            }
        }

        // 아이템 구매
        public int Buy(Item item)
        {
            Gold -= item.Price; // 금화 차감
            return Gold; // 남은 금화 반환
        }

        // 아이템 판매
        public int Sell(Item item)
        {
            Gold += item.Price * 85 / 100; // 85% 가격으로 금화 추가
            return Gold; // 남은 금화 반환
        }

        // 플레이어 이름 변경
        public void ChangeName(string name)
        {
            Name = name; // 이름 변경
        }

        // 플레이어 직업 변경
        public void ChangeJob(EJob job)
        {
            Job = job; // 직업 변경

            switch (job)
            {
                case EJob.전사:
                    Attack = 10.0; // 전사 공격력
                    Defense = 10.0; // 전사 방어력
                    MaxHp = 150.0; // 전사 최대 체력
                    MaxMp = 30.0; // 전사 최대 마나
                    MaxExp = 10; // 전사 최대 경험치
                    break;

                case EJob.궁수:
                    Attack = 13.0; // 궁수 공격력
                    Defense = 7.0; // 궁수 방어력
                    MaxHp = 120.0; // 궁수 최대 체력
                    MaxMp = 60.0; // 궁수 최대 마나
                    MaxExp = 10; // 궁수 최대 경험치
                    break;

                case EJob.마법사:
                    Attack = 15.0; // 마법사 공격력
                    Defense = 5.0; // 마법사 방어력
                    MaxHp = 100.0; // 마법사 최대 체력
                    MaxMp = 80.0; // 마법사 최대 마나
                    MaxExp = 10; // 마법사 최대 경험치
                    break;

                default:
                    Console.WriteLine("알 수 없는 직업입니다."); // 알 수 없는 직업 메시지 출력
                    break;
            }

            Hp = MaxHp; // 현재 체력을 최대 체력으로 설정
            Mp = MaxMp; // 현재 마나를 최대 마나로 설정
        }

        // 금화 추가
        public void AddGold(int amount)
        {
            Gold += amount; // 금화 증가
        }

        // 장비 아이템 추가
        public void AddItem(EquipmentItem equipmentItem)
        {
            if (equipmentItem != null) // 유효한 아이템일 경우
            {
                equipmentInventory.Add(equipmentItem); // 장비 아이템 인벤토리에 추가
            }
            else
            {
                Console.WriteLine("잘못된 아이템입니다."); // 잘못된 아이템 메시지 출력
            }
        }

        // 몬스터 처치 수 증가
        public void IncrementMonsterKills()
        {
            MonsterKills++; // 몬스터 처치 수 증가
        }

        // 플레이어 데이터 설정
        public void SetPlayerData(string name, int level, double attack, double defense, double hp, double maxHp, double mp, double maxMp, int gold, int exp, int maxExp, EJob job, int potions, int monsterKills)
        {
            Name = name; // 이름 설정
            Level = level; // 레벨 설정
            Attack = attack; // 공격력 설정
            Defense = defense; // 방어력 설정
            Hp = hp; // 현재 체력 설정
            MaxHp = maxHp; // 최대 체력 설정
            Mp = mp; // 현재 마나 설정
            MaxMp = maxMp; // 최대 마나 설정
            Gold = gold; // 금화 설정
            Exp = exp; // 경험치 설정
            MaxExp = maxExp; // 최대 경험치 설정
            Job = job; // 직업 설정
            Potions = potions; // 포션 설정
            MonsterKills = monsterKills; // 몬스터 처치 수 설정
        }
    }

    // 플레이어의 직업을 정의하는 열거형
    public enum EJob
    {
        공용,  // 일반 또는 기본 직업
        전사,  // 전사 직업
        궁수,  // 궁수 직업
        마법사 // 마법사 직업
    }
}